using System;
using System.Collections.Generic;
using System.Text;

namespace DroidExplorer.Core.Market {
  public class MarketApplication {

    public MarketApplication ( string name, string description, string author, Uri dlUri, DateTime pubDate, int downloads, decimal rating ) {
      this.Name = name;
      this.Description = description;
      this.PublishDate = pubDate;
      this.Author = author;
      this.Rating = rating;
      this.Downloads = downloads;
      this.DownloadUri = dlUri;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public DateTime PublishDate { get; private set; }
    public string Author { get; private set; }
    public decimal Rating { get; private set; }
    public int Downloads { get; private set; }
    public Uri DownloadUri { get; private set; }

    public override string ToString ( ) {
      return this.Name;
    }
  }
}
