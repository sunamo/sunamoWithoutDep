using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Rss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace SunamoRss
{
    public class RssHelper
    {
        public static List<Tuple<string, string, string, DateTimeOffset>> Latest5PostsFromRss(string filePath)
        {
            List<Tuple<string, string, string, DateTimeOffset>> result = new List<Tuple<string, string, string, DateTimeOffset>>();

            using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings()))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (feedReader.Read().Result)
                {
                    switch (feedReader.ElementType)
                    {
                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = feedReader.ReadItem().Result; //AsyncHelper.ci.GetResult(feedReader.ReadItem());
                            result.Add(new Tuple<string, string, string, DateTimeOffset>(item.Title, item.Links.First().Uri.ToString(), item.Description, item.Published));
                            break;

                            #region MyRegion
                            //// Read category
                            //case SyndicationElementType.Category:
                            //    ISyndicationCategory category = await feedReader.ReadCategory();
                            //    break;

                            //// Read Image
                            //case SyndicationElementType.Image:
                            //    ISyndicationImage image = await feedReader.ReadImage();
                            //    break;
                            //// Read link
                            //case SyndicationElementType.Link:
                            //    ISyndicationLink link = await feedReader.ReadLink();
                            //    break;

                            //// Read Person
                            //case SyndicationElementType.Person:
                            //    ISyndicationPerson person = await feedReader.ReadPerson();
                            //    break;

                            //// Read content
                            //default:
                            //    ISyndicationContent content = await feedReader.ReadContent();
                            //    break; 
                            #endregion
                    }

                    if (result.Count == 5)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        async static Task<List<Tuple<string, string, DateTimeOffset>>> Latest5PostsFromRssAsync(string filePath)
        {
            List<Tuple<string, string, DateTimeOffset>> result = new List<Tuple<string, string, DateTimeOffset>>();

            using (var xmlReader = XmlReader.Create(filePath, new XmlReaderSettings() { Async = true }))
            {
                var feedReader = new RssFeedReader(xmlReader);

                while (await feedReader.Read())
                {
                    switch (feedReader.ElementType)
                    {
                        // Read category
                        case SyndicationElementType.Category:
                            ISyndicationCategory category = await feedReader.ReadCategory();
                            break;

                        // Read Image
                        case SyndicationElementType.Image:
                            ISyndicationImage image = await feedReader.ReadImage();
                            break;

                        // Read Item
                        case SyndicationElementType.Item:
                            ISyndicationItem item = await feedReader.ReadItem();

                            result.Add(new Tuple<string, string, DateTimeOffset>(item.Title, item.Links.First().Uri.ToString(), item.Published));

                            break;

                        // Read link
                        case SyndicationElementType.Link:
                            ISyndicationLink link = await feedReader.ReadLink();
                            break;

                        // Read Person
                        case SyndicationElementType.Person:
                            ISyndicationPerson person = await feedReader.ReadPerson();
                            break;

                        // Read content
                        default:
                            ISyndicationContent content = await feedReader.ReadContent();
                            break;
                    }

                    if (result.Count == 5)
                    {
                        break;
                    }
                }
            }

            return result;
        }
    }
}