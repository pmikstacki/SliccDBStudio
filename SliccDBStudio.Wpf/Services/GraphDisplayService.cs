using System.Collections.ObjectModel;
using SliccDB.Core;
using VisNetwork.Blazor.Models;
using Node = VisNetwork.Blazor.Models.Node;
using Edge = VisNetwork.Blazor.Models.Edge;
using System.Linq;
using System;

namespace SliccDBStudio.Services;

public class GraphDisplayService
{
    public NetworkData GetGraphResult(HashSet<GraphEntity> entities)
    {

        var _data = new NetworkData();
        var colors = new Dictionary<string, string>();
        colors.Add("unknown", GetRandomColor());
        var nodes = entities.Where(en => en is SliccDB.Core.Node).Select(entity =>
        {
            var colorSel = GetRandomColor();

            var color = colors.ContainsKey(entity.Labels.FirstOrDefault() ?? "unknown")
                ? colors[entity.Labels.FirstOrDefault() ?? "unknown"]
                : colorSel; // = "#A197B9"
            colors.TryAdd(entity.Labels.FirstOrDefault() ?? "unknown", colorSel);

            if (entity is SliccDB.Core.Node node)
            {
                return new Node(node.Hash, string.Join(", ", node.Labels.Select(l => $":{l}")), 1, "circle")
                {
                    Color = new NodeColorType()
                    {
                        Background = color
                    }
                };

            }

            return null;
        });
        var edges = entities.Where(ed => ed is SliccDB.Core.Relation).Select(entity =>
        {
            if (entity is SliccDB.Core.Relation relation)
            {
                var edge = new Edge(relation.SourceHash, relation.TargetHash,
                    string.Join(", ", relation.Labels.Select(l => $":{l}"))) {Id = relation.Hash };
                edge.Label = relation.RelationName;
                return edge;
            }

            return null;
        });

        _data = new NetworkData
        {
            Edges = new ReadOnlyCollection<Edge>(edges.ToList()),
            Nodes = new ReadOnlyCollection<Node>(nodes.ToList())
        };

        return _data;

    }

    public string GetRandomColor()
    {
        var random = new Random();

        var color = String.Format("#{0:X6}", random.Next(0x1000000));
        System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml(color);
        if (col.R * 0.2126 + col.G * 0.7152 + col.B * 0.0722 < 255 / 2)
        {
            return GetRandomColor();
        }
        else
        {
            return color;
        }
    }
}