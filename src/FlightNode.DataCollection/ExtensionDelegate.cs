using FligthNode.Common.Api.Controllers;
using System;
using Microsoft.AspNet.Identity;

namespace FlightNode.DataCollection
{
    /// <summary>
    /// Contains delegates pointing to extension methods, which can be overridden for unit test purposes.
    /// </summary>
    public static class ExtensionDelegate
    {

        public static Func<LoggingController, int> LookupUserIdDelegate;

        public static int LookupUserId(this LoggingController controller)
        {
            return LookupUserIdDelegate(controller);
        }



        //public static Action<IModifiable, object> SetModifiedStateDelegate;

        //public static void SetModifiedStateOn(this IModifiable modifiable, object entry)
        //{
        //    SetModifiedStateDelegate(modifiable, entry);
        //}



        static ExtensionDelegate()
        {
            Init();
        }




        public static void Init()
        {
            LookupUserIdDelegate = (LoggingController controller) =>
            {
                return controller.User.Identity.GetUserId<int>();
            };

            //SetModifiedStateDelegate = (IModifiable persistenceLayer, object input) =>
            //{
            //    persistenceLayer.Entry(input).State = System.Data.Entity.EntityState.Modified;
            //};
        }

    }
}
