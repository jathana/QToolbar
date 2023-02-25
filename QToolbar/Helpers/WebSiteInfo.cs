﻿using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class WebSiteInfo
   {

      public class SimpleProperty
      {
         #region fields
         private string _Name;
         private string _Value;
         #endregion

         #region properties
         public string Name
         {
            get
            {
               return _Name;
            }

            set
            {
               _Name = value;
            }
         }
         public string Value
         {
            get
            {
               return _Value;
            }

            set
            {
               _Value = value;
            }
         }
         public List<SimpleProperty> Properties { get; set; } = new List<SimpleProperty>();


         #endregion
      }

      public WebSiteTypeEnum WebSiteType { get; set; }
      public string Name { get; set; }
      public string Url { get; set; }

      public string Host { get; set; }
      public string Port { get; set; }
      
      public string Protocol { get; set; }

      public List<SimpleProperty> Properties { get; }

      public string PhysicalPath { get; set; }
      

      public WebSiteInfo(string host, Site site)
      {
         Properties = new List<SimpleProperty>();

         Host = host;
         Name = GetSiteName(site);
         Port = GetSitePort(site);
         Protocol = GetSiteProtocol(site);
         PhysicalPath = site.Applications["/"].VirtualDirectories["/"].PhysicalPath;
         UNCPath = $"\\\\{Host}\\{PhysicalPath.Replace(":","$")}";
      }

      public override string ToString()
      {
         return $"{Protocol}://{Host}/{Name}:{Port}";
      }

      public string UNCPath {get;}
      protected void AddProperty(string name, string value, List<SimpleProperty> children)
      {

         Properties.Add(new SimpleProperty()
         {
            Name = name,
            Value = value,
            Properties = children,

         });
      }

      private string GetSitePort(Site site)
      {
         string retVal = string.Empty;

         if (site != null && site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
         {
            retVal = (site.Bindings[0]).EndPoint.Port.ToString();
         }
         return retVal;
      }

      private string GetSiteProtocol(Site site)
      {
         string retVal = string.Empty;

         if (site != null && site.Bindings != null && site.Bindings.Count > 0 && (site.Bindings[0]).EndPoint != null)
         {
            retVal = site.Bindings[0].Protocol;
         }
         return retVal;
      }


      private string GetSiteName(Site site)
      {
         string retVal = string.Empty;

         if (site != null)
         {
            retVal = site.Name;
         }
         return retVal;
      }


   }
}
