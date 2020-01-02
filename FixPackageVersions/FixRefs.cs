using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace FixPackageVersions
{
    public static class FixRefs
    {
        public static string Fix(string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentNullException(nameof(xml));

            var nodes = ParseXml(xml);

            var sb = new System.Text.StringBuilder();

            foreach (var node in nodes)
            {
                var fixedNode = FixNode(node);
                sb.AppendLine(fixedNode.ToString());
            }

            return sb.ToString();
        }

        private static IEnumerable<XNode> ParseXml(string xml)
        {
            var settings = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
                IgnoreWhitespace = true
            };

            using var stringReader = new StringReader(xml);
            using var xmlReader = XmlReader.Create(stringReader, settings);
            xmlReader.MoveToContent();
            while (xmlReader.ReadState != ReadState.EndOfFile)
            {
                yield return XNode.ReadFrom(xmlReader);
            }
        }

        private static XNode FixNode(XNode node)
        {
            if (node.NodeType != XmlNodeType.Element) return node;
            XElement e = (XElement)node;
            if (e.Name != "PackageReference") return node;
            // does it have Version attribute?
            var ver = e.Attribute("Version");
            if (ver != null) return node;

            var verNode = e.Descendants("Version").FirstOrDefault();
            if (verNode == null) return node;

            var version = verNode.Value;
            e.RemoveNodes();
            e.Add(new XAttribute("Version", version));
            return e;
        }
    }
}
