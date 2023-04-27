using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QToolbar.Helpers
{
   public class ProcessingInfoCollectedEventArgs : EventArgs
   {
      public int HostsCount { get; set; }
      public int CurrentHostLoadedNumber { get; set; }

      public int HostSitesCount { get; set; }
      public int HostCurrentSiteLoadedNumber { get; set; }

      public ProcessingInfoCollectedEventArgs(int hostsCount, int currentHostLoadedNumber, int hostSitesCount, int hostCurrentSiteLoadedNumber)
      {
         HostsCount=hostsCount;
         CurrentHostLoadedNumber=currentHostLoadedNumber;
         HostSitesCount=hostSitesCount;
         HostCurrentSiteLoadedNumber=hostCurrentSiteLoadedNumber;
      }
   }
}
