using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace mialco.shopping.connector.GoogleAdFeed
{
	/// <summary>
	/// Class to write the Google XML Feed to a file
	/// </summary>
	public class GoogleXmlFileWriter: IDisposable
	{
		private const string FeedRootElement = "rss";
		private const string ChannelTag = "channel";
		private const string TitleTag = "title";
		private const string TitleValue = "Amore- T Shirts";
		private const string LinkTag = "link";
		private const string LinkValue = "http://www.amoretees.com/";
		private const string DescriptionTag = "description";
		private const string DescriptionValue = "Amore - T Shirts store";
		private const string AttributePrefix = "g";
		private const string GoogleNameSpace = @"xmlns:g=""http://base.google.com/ns/1.0""";
		private const string ItemTag ="item";
		private readonly string _outputFileName;
		private XmlWriter _xmlWriter;
		private int _writerStatus = 0;
		private bool _itemOpen = false;
		private bool _rootOpen = false;


		public GoogleXmlFileWriter(string outputFileName)
		{
			_outputFileName = outputFileName;
		}	


		/// <summary>
		/// Starts a new XML feed file of the specified root element
		/// </summary>
		internal void OpenFeed(string prefix, string nameSpace)	
		{
			try
			{
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = "\t";
				_xmlWriter = XmlWriter.Create(_outputFileName, settings);
				_writerStatus = 1;
				 _xmlWriter.WriteStartDocument();
				_xmlWriter.WriteStartElement(prefix, FeedRootElement,nameSpace);
				//_xmlWriter.WriteQualifiedName("QualifiedName", GoogleNameSpace);
				
				_xmlWriter.WriteStartElement("channel");
				_xmlWriter.WriteElementString(TitleTag, TitleValue);
				_xmlWriter.WriteElementString(LinkTag, LinkValue);
				_xmlWriter.WriteElementString(DescriptionTag, DescriptionValue);
				_rootOpen = true;
				//_xmlWriter.WriteEndElement();
			}
			catch (Exception)
			{
				//TODO: Handle this Exception
				throw;
			}
		}


		/// <summary>
		/// Writes the tak for a new item, closing the previous one if tehre is one opened already
		/// </summary>
		internal void  StartItem()
		{
			EndItem();
			_xmlWriter.WriteStartElement(ItemTag);
			_itemOpen = true;
		}

		/// <summary>
		/// Closes previously opened root eement
		/// </summary>
		internal void CloseFeed()
		{
			if (_itemOpen)
			{
				_xmlWriter.WriteFullEndElement();
				_itemOpen = false;
			}

			if (_rootOpen)
			{
				_xmlWriter.WriteFullEndElement();
				_rootOpen = false;
			}
			_xmlWriter.Flush();
			_xmlWriter.Dispose();
			_xmlWriter = null;
		}

		internal void WriteItemElement(string tag, string value, string prefix = "", string nameSpace="")
		{
			if (prefix == string.Empty || nameSpace==string.Empty)
			{
				_xmlWriter.WriteElementString(tag, value);
			}
			else
			{
				_xmlWriter.WriteElementString(prefix, tag, nameSpace, value);
			}
		}

		/// <summary>
		/// Closes the previously opened item element
		/// </summary>
		internal void EndItem()
		{
			if (_itemOpen)
			{
				_xmlWriter.WriteFullEndElement();
				_itemOpen = false;
			}

		}

		
		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		public int WriterStatus {get => _writerStatus; private set => _writerStatus = value; }

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
					try
					{
						if (_xmlWriter != null)
						{
							_xmlWriter.Flush();
							_xmlWriter.Close();
							_xmlWriter.Dispose();
						}

					}
					catch (Exception)
					{
						if (_xmlWriter != null)
						{
							_xmlWriter.Dispose();
						}
					}
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~GoogleXmlFileWriter() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

	}
}
