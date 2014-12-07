using Mvc.Mailer;
using RepositoryPattern.Models;
using System.Collections.Generic;

namespace RepositoryPattern.Mailers
{ 
    public interface IUserMailer
    {
			MvcMailMessage Welcome();
			MvcMailMessage PassWordReset();
            MvcMailMessage EmailWishList(IEnumerable<EmailWishList> items);
	}
}