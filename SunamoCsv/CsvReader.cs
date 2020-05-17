
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;


    /// <summary>
/// Class to read csv content from various sources
/// Downloaded from http://www.codeproject.com/Articles/86973/C-CSV-Reader-and-Writer
    /// </summary>
    public sealed class CsvReader : IDisposable
    {

    static Type type = typeof(CsvReader);

        #region Members

        private FileStream _fileStream;
        private Stream _stream;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private Stream _memoryStream;
        private Encoding _encoding;
        private readonly StringBuilder _columnBuilder = new StringBuilder(100);
        private readonly TypeSource _type = TypeSource.File;

    #endregion Members

    /// <summary>
    /// Use CsvFile.DateTimes instead
    /// </summary>
    /// <param name="v"></param>
    public static List<DateTime?> DateTime(CsvFile file, int v)
    {
        return file.DateTimes(v);
    }

    public static List<string> Strings(CsvFile file, int v)
    {
        return file.Strings(v);
    }

        #region Properties

    /// <summary>
    /// Gets or sets whether column values should be trimmed
    /// </summary>
    public bool TrimColumns { get; set; }

        /// <summary>
        /// Gets or sets whether the csv file has a header row
        /// </summary>
        public bool HasHeaderRow { get; set; }

        /// <summary>
        /// Returns a collection of fields or null if no record has been read
        /// </summary>
        public List<string> Fields { get; private set; }



    /// <summary>
    /// Gets the field count or returns null if no fields have been read
    /// </summary>
    public int? FieldCount
        {
            get
            {
                return (Fields != null ? Fields.Count : (int?)null);
            }
        }

        #endregion Properties

        #region Enums

        /// <summary>
        /// Type enum
        /// </summary>
        private enum TypeSource
        {
            File,
            Stream,
            String
        }

        #endregion Enums

        #region Constructors

        /// <summary>
        /// Initialises the reader to work from a file
        /// </summary>
        /// <param name="filePath">File path</param>
        public CsvReader(string filePath)
        {
            _type = TypeSource.File;
            Initialise(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// Initialises the reader to work from a file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="encoding">Encoding</param>
        public CsvReader(string filePath, Encoding encoding)
        {
            _type = TypeSource.File;
            Initialise(filePath, encoding);
        }

        /// <summary>
        /// Initialises the reader to work from an existing stream
        /// </summary>
        /// <param name="stream">Stream</param>
        public CsvReader(Stream stream)
        {
            _type = TypeSource.Stream;
            Initialise(stream, Encoding.UTF8);
        }

        /// <summary>
        /// Initialises the reader to work from an existing stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="encoding">Encoding</param>
        public CsvReader(Stream stream, Encoding encoding)
        {
            _type = TypeSource.Stream;
            Initialise(stream, encoding);
        }

        /// <summary>
        /// Initialises the reader to work from a csv string
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="csvContent"></param>
        public CsvReader(Encoding encoding, string csvContent)
        {
            _type = TypeSource.String;
            Initialise(encoding, csvContent);  
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initialises the public class to use a file
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="encoding"></param>
        private void Initialise(string filePath, Encoding encoding)
        {
            if (!FS.ExistsFile(filePath))
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),SH.Format2("The file '{0}' does not exist.", filePath));

            _fileStream = File.OpenRead(filePath);
            Initialise(_fileStream, encoding);
        }

        /// <summary>
        /// Initialises the public class to use a stream
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="encoding"></param>
        private void Initialise(Stream stream, Encoding encoding)
        {
            if (stream == null)
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"The supplied stream is null" + ".");

            _stream = stream;
            _stream.Position = 0;
            _encoding = (encoding ?? Encoding.UTF8);
            _streamReader = new StreamReader(_stream, _encoding);
        }

        /// <summary>
        /// Initialies the public class to use a string
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="csvContent"></param>
        private void Initialise(Encoding encoding, string csvContent)
        {
            if (csvContent == null)
                ThrowExceptions.Custom(Exc.GetStackTrace(), type, Exc.CallingMethod(),"The supplied csvContent is null" + ".");

            _encoding = (encoding ?? Encoding.UTF8);

            _memoryStream = new MemoryStream(csvContent.Length);
            _streamWriter = new StreamWriter(_memoryStream);
            _streamWriter.Write(csvContent);
            _streamWriter.Flush();
            Initialise(_memoryStream, encoding);           
        }

        /// <summary>
        /// Reads the next record
        /// </summary>
        /// <returns>True if a record was successfuly read, otherwise false</returns>
        public bool ReadNextRecord()
        {
            Fields = null;
            string line = _streamReader.ReadLine();

            if (line == null)
                return false;

            ParseLine(line);
            return true;
        }

        /// <summary>
        /// Reads a csv file format into a data table.  This method
        /// will always assume that the table has a header row as this will be used
        /// to determine the columns.
        /// </summary>
        public DataTable ReadIntoDataTable()
        {
            return ReadIntoDataTable(new System.Type[] {});
        }

        /// <summary>
        /// Reads a csv file format into a data table.  This method
        /// will always assume that the table has a header row as this will be used
        /// to determine the columns.
        /// </summary>
        /// <param name="columnTypes">Array of column types</param>
        public DataTable ReadIntoDataTable(System.Type[] columnTypes)
        {
            DataTable dataTable = new DataTable();
            bool addedHeader = false;
            _stream.Position = 0;

            while (ReadNextRecord())
            {
                if (!addedHeader)
                {
                    for (int i = 0; i < Fields.Count; i++)
                        dataTable.Columns.Add(Fields[i], (columnTypes.Length > 0 ? columnTypes[i] : typeof(string)));

                    addedHeader = true;
                    continue;
                }

                DataRow row = dataTable.NewRow();

                for (int i = 0; i < Fields.Count; i++)
                    row[i] = Fields[i];

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

    public static char delimiter = AllChars.comma;

        /// <summary>
        /// Parses a csv line
        /// </summary>
        /// <param name="line">Line</param>
        private void ParseLine(string line)
        {
            Fields = new List<string>();
            bool inColumn = false;
            bool inQuotes = false;
            _columnBuilder.Remove(0, _columnBuilder.Length);

            // Iterate through every character in the line
            for (int i = 0; i < line.Length; i++)
            {
                char character = line[i];

                // If we are not currently inside a column
                if (!inColumn)
                {
                    if (character == AllChars.qm)
                        inQuotes = true;
                    else
                        _columnBuilder.Append(character);

                    inColumn = true;
                    continue;
                }

                // If we are in between double quotes
                if (inQuotes)
                {
                    if (character == AllChars.qm && ((line.Length > (i + 1) && line[i + 1] == delimiter) || ((i + 1) == line.Length)))
                    {
                        inQuotes = false;
                        inColumn = false;
                        i++;
                    }
                    else if (character == AllChars.qm && line.Length > (i + 1) && line[i + 1] == AllChars.qm)
                        i++;
                }
                else if (character == delimiter)
                    inColumn = false;

                // If we are no longer in the column clear the builder and add the columns to the list
                if (!inColumn)
                {
                    Fields.Add(TrimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());
                    _columnBuilder.Remove(0, _columnBuilder.Length);
                }
                else // append the current column
                    _columnBuilder.Append(character);
            }

            // If we are still inside a column add a new one
            if (inColumn)
                Fields.Add(TrimColumns ? _columnBuilder.ToString().Trim() : _columnBuilder.ToString());   
        }

        /// <summary>
        /// Disposes of all unmanaged resources
        /// </summary>
        public void Dispose()
        {
            if (_streamReader != null)
            {
                _streamReader.Close();
                _streamReader.Dispose();
            }

            if (_streamWriter != null)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
            }

            if (_memoryStream != null)
            {
                _memoryStream.Close();
                _memoryStream.Dispose();
            }

            if (_fileStream != null)
            {
                _fileStream.Close();
                _fileStream.Dispose();
            }

            if ((_type == TypeSource.String || _type == TypeSource.File) && _stream != null)
            {
                _stream.Close();
                _stream.Dispose();
            }
        }

        #endregion Methods

    }