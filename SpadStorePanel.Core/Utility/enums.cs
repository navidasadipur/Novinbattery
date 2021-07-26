using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpadStorePanel.Core.Utility
{
    public enum DiscountType
    {
        Percentage = 1,
        Amount = 2
    }
    public enum GeoDivisionType
    {
        Country = 0,
        State = 1,
        City = 2,
    }
    public enum StaticContents
    {
        Phone = 1002,
        Map = 1007,
        Address = 1001,
        Email = 1003,
        Youtube = 1008,
        Instagram = 1009,
        Twitter = 1011,
        Pinterest = 1012,
        Facebook = 1010,
        BlogImage = 1013,
        ContactInfo = 1014,

        


        HeaderBackGroundImage = 13,
    }

    public enum StaticContentTypes
    {
        HeaderFooter = 9,
        About = 13,
        AboutProperties,

        HomeTopSlider = 17,
        Contact = 2,

        Guide = 9,
        Popup = 11,
        PageBanner = 12,
        OurServices = 3,

        BuyingStepsSection = 6,

        HomeTopBaner = 15,
        HomeMidleBaner = 17,

    }

    public enum PaymentStatus
    {
        Unprocessed = 1,
        Failed = 2,
        Succeed = 3,
        Expired = 4
    }

    public enum AditionalFeatureType
    {
        Volume = 1
    }
}
