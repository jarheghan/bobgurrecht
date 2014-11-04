using RepositoryPattern.Data;
using RepositoryPattern.Model.Catalog;
using RepositoryPattern.Model.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace RepositoryPattern.Areas.Admin.Infacstructure
{
    public static class CommonHelpers
    {

        public static ICategoryRepository CategoryRepository
        {
            get;
            set;
        }
        public static ICustomerInfoRepository CustomerInfoRepo { get; set; }
       
       
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
                str = String.Concat(str, random.Next(10).ToString());
            return str;
        }

        /// <summary>
        /// Returns an random interger number within a specified rage
        /// </summary>
        /// <param name="min">Minimum number</param>
        /// <param name="max">Maximum number</param>
        /// <returns>Result</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }


        public static SelectList GetCategory()
        {
            CategoryRepository = new CategoryDataMapper();
            List<Category> cat = new List<Category>();
            var category = CategoryRepository.GetAllCategories();
            var subCat = category.Where(x => x.ParentCategoryID != null);
            var parentCategory = category.Where(x => x.ParentCategoryID == null);
            foreach (var parentcat in parentCategory)
            {
                cat.Add(parentcat);
                foreach (var mysubcat in subCat)
                {
                    if (mysubcat.ParentCategoryID == parentcat.ID)
                    {
                        mysubcat.Name = parentcat.Name + " >> " + mysubcat.Name;
                        cat.Add(mysubcat);

                        if (category.Any(x => x.ParentCategoryID == mysubcat.ID) == true)
                        {
                            var subsubCat = category.Where(y => y.ParentCategoryID == mysubcat.ID);
                            foreach (var s in subsubCat)
                            {
                                if(cat.Exists(x => x.ID != s.ID))
                                {
                                s.Name = mysubcat.Name + " >> " + s.Name;
                                cat.Add(s);
                                }
                            }
                           
                        }
                    }

                }
            }
            SelectList sl = new SelectList(cat, "ID", "Name");
            return sl;
        }

        public static List<Category> GetAllCategory()
        {
            CategoryRepository = new CategoryDataMapper();
            List<Category> cat = new List<Category>();
            var category = CategoryRepository.GetAllCategories();
            var subCat = category.Where(x => x.ParentCategoryID != null);
            var parentCategory = category.Where(x => x.ParentCategoryID == null);
            foreach (var parentcat in parentCategory)
            {
                cat.Add(parentcat);
                foreach (var mysubcat in subCat)
                {
                    if (mysubcat.ParentCategoryID == parentcat.ID)
                    {
                        mysubcat.Name = parentcat.Name + " >> " + mysubcat.Name;
                        cat.Add(mysubcat);

                        if (category.Any(x => x.ParentCategoryID == mysubcat.ID) == true)
                        {
                            var subsubCat = category.Where(y => y.ParentCategoryID == mysubcat.ID);
                            foreach (var s in subsubCat)
                            {
                                if (cat.Exists(x => x.ID != s.ID))
                                {
                                    s.Name = mysubcat.Name + " >> " + s.Name;
                                    cat.Add(s);
                                }
                            }

                        }
                    }

                }
            }

            return cat;
        }


        public static SelectList GetAllCountry()
        {
            CustomerInfoRepo = new CustomerInfoDataMapper();
            var country = CustomerInfoRepo.GetAllCountries().OrderBy(x => x.Name);
            SelectList sl = new SelectList(country, "ISO3", "Name");
            return sl;
        }

        public static SelectList GetAllStates()
        {
            CustomerInfoRepo = new CustomerInfoDataMapper();
            var country = CustomerInfoRepo.GetAllStates().OrderBy(x => x.Name);
            SelectList sl = new SelectList(country, "AbbreviatedName", "Name");
            return sl;
        }
    }
}