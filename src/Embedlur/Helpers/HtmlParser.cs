using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Embedlur.Helpers
{
    public class HtmlParser : IHtmlParser
    {
        // <meta[\s]+((\S+)=["']((?:.(?!["']?\s+(?:\S+)=|[>"']))+.)["']+[\s]*)*/>
        private readonly Regex _metaRegex = new Regex("<meta[\\s]+((\\S+)=[\"']((?:.(?![\"']?\\s+(?:\\S+)=|[>\"']))+.)[\"']+[\\s]*)*/>");

        public List<HtmlMetaTag> ParseMetaTags(string html)
        {
            var result = new List<HtmlMetaTag>();

            if (string.IsNullOrEmpty(html))
                return result;

            var matches = _metaRegex.Matches(html);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    if(match.Groups.Count != 4) throw new Exception("It should be...");

                    var nameCaptures = match.Groups[2].Captures;
                    var valueCaptures = match.Groups[3].Captures;

                    if(nameCaptures.Count != valueCaptures.Count)
                        throw new Exception("There should be a matching value caught for each name");

                    if(nameCaptures.Count == 0) continue;

                    var metaTag = new HtmlMetaTag();

                    for (var index = 0; index < nameCaptures.Count; index++)
                    {
                        switch (nameCaptures[index].Value)
                        {
                            case "property":
                                metaTag.Property = valueCaptures[index].Value;
                                break;
                            case "name":
                                metaTag.Name = valueCaptures[index].Value;
                                break;
                            case "content":
                                metaTag.Content = valueCaptures[index].Value;
                                break;
                        }
                    }

                   

                    //foreach (Group group in match.Groups)
                    //{
                    //    if (index == 0)
                    //    {
                    //        index++;
                    //        continue; // first item, we don't care about
                    //    }

                    //    switch (group.Value)
                    //    {
                    //        case "property":
                    //        case "name":
                    //        case "content":
                    //            currentKey = group.Value;
                    //            break;
                    //        default:
                    //            if(string.IsNullOrEmpty(currentKey))
                    //                throw new Exception("We should have a key group value before any other value.");

                    //            switch (currentKey)
                    //            {
                    //                case "property":
                    //                    metaTag.Property = group.Value;
                    //                    break;
                    //                case "name":
                    //                    metaTag.Name = group.Value;
                    //                    break;
                    //                case "content":
                    //                    metaTag.Content = group.Value;
                    //                    break;
                    //                default:
                    //                    throw new Exception("Unknown key");
                    //            }

                    //            break;
                    //    }

                    //    index++;
                    //}

                    result.Add(metaTag);
                }
            }

            return result;
        }
    }
}
