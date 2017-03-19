using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels.Utility;
using Nancy.Json;

namespace Kontur.GameStats.Server.NancyModules.NancyConfiguration
{
  public class DateTimeCustomConverter: JavaScriptConverter
  {
    public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
    {
      if (obj is DateTime)
      {
        DateTime date = (DateTime)obj;

        var json = new Dictionary<string, object> {["timestamp"] = date.ToUtcString()};


        return json;
      }

      return null;
    }

    public override object Deserialize(IDictionary<string, object> json, Type type, JavaScriptSerializer serializer)
    {
      if (type == typeof(DateTime))
      {
        object timestamp;

        json.TryGetValue("timestamp", out timestamp);

        if ((timestamp is DateTime)) return timestamp;
      }

      return null;
    }

    public override IEnumerable<Type> SupportedTypes { get { yield return typeof (DateTime); } }
  }
}