using Mvc.Mailer;
using RepositoryPattern.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace RepositoryPattern.Mailers
{ 
    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Welcome()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Welcome";
				x.ViewName = "Welcome";
				x.To.Add("some-email@example.com");
			});
		}
 
		public virtual MvcMailMessage PassWordReset()
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "PassWordReset";
				x.ViewName = "PassWordReset";
				x.To.Add("some-email@example.com");
			});
		}

        public virtual MvcMailMessage EmailWishList(IEnumerable<EmailWishList> items)
        {
            ViewData = new ViewDataDictionary(items);
            ViewBag.Name = items.Select(x => x.FullName).FirstOrDefault();
            return Populate(x =>
                {
                    x.Subject = "Items Order By Customer";
                    x.ViewName = "EmailWishList";
                    x.To.Add("dscrollj@gmail.com");
                    
                });
        }
 	}
}