using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Wahlen2017
{

    public class Wahllokal
    {

        public Byte    WahllokalId  { get; set; }
        public String  Name         { get; set; }
        public Double  lng          { get; set; }
        public Double  lat          { get; set; }
        public String  Raum         { get; set; }
        public String  Adresse      { get; set; }
        public Boolean bgerecht     { get; set; }

        // {
        //   "type": "Feature",
        //   "geometry": {
        //     "type": "Point",
        //     "coordinates": [
        //       11.6319650729039,
        //       50.8877710358353
        //     ]
        //   },
        //   "properties": {
        //     "id": 1127,
        //     "gid": 1127,
        //     "the_geom": "0101000020E86400001E21EEBE77E82441DAEDE13171845541",
        //     "name": "Gemeindesaal Drackendorf"
        //   }
        // },

        public JObject ToJSON()

            => new JObject(

                   new JProperty("type", "Feature"),

                   new JProperty("geometry", new JObject(
                       new JProperty("type",                "Point"),
                       new JProperty("coordinates",         new JArray(lng, lat))
                   )),

                   new JProperty("properties", new JObject(
                       new JProperty("Wahllokal",           WahllokalId),
                       new JProperty("Name",                Name),
                       new JProperty("Raum",                Raum),
                       new JProperty("Adresse",             Adresse),
                       new JProperty("behindertengerecht",  bgerecht)
                   ))

               );

        public override String ToString()

            => String.Concat(WahllokalId,                       '\t',
                             Name,                              '\t',
                             lng.ToString().Replace(",", "."),  '\t',
                             lat.ToString().Replace(",", "."),  '\t',
                             Raum,                              '\t',
                             Adresse,                           '\t',
                             bgerecht);

    }

    public class Addresse
    {

        public String  Strasse     { get; set; }
        public String  Hausnummer  { get; set; }
        public String  PLZ         { get; set; }
        public Double  lng         { get; set; }
        public Double  lat         { get; set; }
        public Byte?   Wahllokal   { get; set; }


        public JObject ToJSON()

            => new JObject(

                   new JProperty("type", "Feature"),

                   new JProperty("geometry", new JObject(
                       new JProperty("type",                "Point"),
                       new JProperty("coordinates",         new JArray(lng, lat))
                   )),

                   new JProperty("properties", new JObject(
                       new JProperty("Strasse",     Strasse),
                       new JProperty("Hausnummer",  Hausnummer),
                       new JProperty("PLZ",         PLZ),
                       new JProperty("Wahllokal",   Wahllokal)
                   ))

               );

        public override String ToString()

            => String.Concat(Strasse,                           '\t',
                             Hausnummer,                        '\t',
                             PLZ,                               '\t',
                             lng.ToString().Replace(",", "."),  '\t',
                             lat.ToString().Replace(",", "."),  '\t',
                             Wahllokal);

    }


    public class Program
    {
        public static void Main(string[] args)
        {

            String[] elements = null;
            //var Strassennamen = new List<String>();

            //foreach (var line in File.ReadLines("Strassennamen.txt"))
            //{

            //    elements = line.Split('\t');

            //    var htc = new HttpClient();
            //    var req = htc.GetStringAsync("http://statistik.jena.de/statistik/wahl/lokal.php?anfbuchst=" + elements[0].Substring(0, 1).ToUpper() + "&strid_wahl=" + elements[1]).Result;
            //    var han = new List<String>();

            //    var match1 = Regex.Match(req, "nr_wahl=([0-9a-zA-Z]+)\\&sb_wahl=([0-9]+)");
            //    while (match1.Success)
            //    {
            //        han.Add(match1.Groups[1].Value + ">" + match1.Groups[2].Value);
            //        match1 = match1.NextMatch();
            //    }

            //    Strassennamen.Add(elements[0] + "\t" + han.Aggregate((a, b) => a + "\t" + b));

            //}

            //File.WriteAllText("StrassennamenHausnummernWahllokale.txt", Strassennamen.Aggregate((a, b) => a + Environment.NewLine + b));


            //var Wahllokale = new List<String>();

            //foreach (var wahllokal in Enumerable.Range(1, 100))
            //{

            //    var htc = new HttpClient();
            //    var req = htc.GetStringAsync("http://statistik.jena.de/statistik/wahl/lokal.php?anfbuchst=A&strid_wahl=1&nr_wahl=6&sb_wahl=" + wahllokal).Result;

            //    // Geb&auml;ude: <b>FFW Lichtenhain</b>, Raum: <b>Schulungsraum 1. Etage</b><br>
            //    // Anschrift: <b>Am Hirschberge 9</b>, behindertengerechter Zugang: <b>nein<br><br>     </td></tr></table>

            //    var match2 = Regex.Match(req, "Geb&auml;ude: \\<b\\>([^\\<]+)\\</b\\>, Raum: \\<b\\>([^\\<]+)\\</b\\>");
            //    if (match2.Success)
            //    {
            //    }

            //    var match3 = Regex.Match(req, "Anschrift: \\<b\\>([^\\<]+)\\</b\\>, behindertengerechter Zugang: \\<b\\>([^\\<]+)\\<br\\>");
            //    if (match3.Success)
            //    {
            //    }

            //    Wahllokale.Add(wahllokal + "\t" + match2.Groups[1].Value + "\t" + match2.Groups[2].Value + "\t" + match3.Groups[1].Value + "\t" + match3.Groups[2].Value);

            //}

            //File.WriteAllText("Wahllokale.txt", Wahllokale.Aggregate((a, b) => a + Environment.NewLine + b));


            //var Wahllokale = new List<Wahllokal>();

            //foreach (var wahllokalTXT in File.ReadAllLines("Wahllokale.txt"))
            //{

            //    elements = wahllokalTXT.Split('\t');

            //    Wahllokale.Add(new Wahllokal() {
            //                       id        = Byte.Parse(elements[0]),
            //                       name      = elements[1],
            //                       raum      = elements[2],
            //                       adresse   = elements[3],
            //                       bgerecht  = elements[4] == "ja"
            //                   });

            //}



            //var WahllokaleJSON  = JObject.Parse(File.ReadAllText("Wahllokale.geojson"));

            //foreach (var wahllokalJSON in (WahllokaleJSON["features"] as JArray))
            //{

            //    var lng  = wahllokalJSON["geometry"]  ["coordinates"][0].Value<String>();
            //    var lat  = wahllokalJSON["geometry"]  ["coordinates"][1].Value<String>();
            //    var name = wahllokalJSON["properties"]["name"       ].   Value<String>();

            //    foreach (var wks in Wahllokale.Where(_ => _.name == name))
            //    {
            //        wks.lng = Double.Parse(lng.Replace(".", ","));
            //        wks.lat = Double.Parse(lat.Replace(".", ","));
            //    }

            //}


            //File.WriteAllLines("Wahllokale2.txt",     Wahllokale.OrderBy(_ => _.id).
            //                                                     Select (_ => _.ToString()));

            //File.WriteAllText("Wahllokale2.geojson",  new JObject(
            //                                              new JProperty("type", "FeatureCollection"),
            //                                              new JProperty("features", new JArray(
            //                                                  Wahllokale.OrderBy(_ => _.id).
            //                                                     Select (_ => _.ToJSON())
            //                                              ))
            //                                          ).ToString());


            //var AdressenJSON  = JObject.Parse(File.ReadAllText("Adressen.geojson"));
            //var Adressen      = new List<Addresse>();

            //foreach (var AdresseJSON in (AdressenJSON["features"] as JArray))
            //{

            //    Adressen.Add(new Addresse() {
            //                     Strasse     = AdresseJSON["properties"]["strasse"   ].Value<String>().Replace("str.", "strasse").Replace("Str.", "Strasse"),
            //                     Hausnummer  = AdresseJSON["properties"]["hausnummer"].Value<String>(),
            //                     PLZ         = AdresseJSON["properties"]["plz"       ].Value<String>(),
            //                     lng         = Double.Parse(AdresseJSON["geometry"]["coordinates"][0].Value<String>().Replace(".", ",")),
            //                     lat         = Double.Parse(AdresseJSON["geometry"]["coordinates"][1].Value<String>().Replace(".", ","))
            //                 });

            //}

            //foreach (var addr in File.ReadLines("StrassennamenHausnummernWahllokale.csv"))
            //{

            //    elements = addr.Split('\t', '>');

            //    foreach (var addi in Adressen.Where(_ => _.Strasse == elements[0]))
            //    {

            //        for (var pos = 1; pos < elements.Length; pos = pos + 2)
            //        {

            //            if (elements[pos] == addi.Hausnummer)
            //                addi.Wahllokal = Byte.Parse(elements[pos + 1]);

            //        }

            //    }

            //}

            //foreach (var addi in Adressen.Where(_ => !_.Wahllokal.HasValue))
            //{
            //}


            //File.WriteAllLines("StrassennamenHausnummernPLZGeoWahllokale.csv", Adressen.Select(_ => _.ToString()));

            //for (var wk = 1; wk<90; wk++)
            //    File.WriteAllText("Wahllokal" + wk + ".geojson",  new JObject(
            //                                                          new JProperty("type",     "FeatureCollection"),
            //                                                          new JProperty("features",  new JArray(
            //                                                              Adressen.
            //                                                                  Where  (_ => _.Wahllokal == wk).
            //                                                                  OrderBy(_ => _.Strasse + _.Hausnummer).
            //                                                                  Select (_ => _.ToJSON())
            //                                                          ))
            //                                                      ).ToString());


        }
    }
}
