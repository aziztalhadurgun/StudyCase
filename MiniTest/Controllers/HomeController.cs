using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.MappingViews;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiniTest.Models;

namespace MiniTest.Controllers
{
    public class HomeController : Controller
    {

        DataContext db = new DataContext();

        public ActionResult Index()
        {
            if (Session["userId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }


        public JsonResult forms(int? id)
        {
            if (Session["userId"] != null)
            {
                //db = new DataContext();

                var userId = Session["userId"];
                if (id != null)
                {
                    var forms = db.Forms.FirstOrDefault();
                    if (forms != null)
                    {
                        var form = new Form()
                        {
                            Id = forms.Id,
                            Name = forms.Name,
                            Description = forms.Description,
                            CreatedAt = forms.CreatedAt.Date,
                            CreatedBy = forms.CreatedBy,
                            Fields = new Field()
                            {
                                Name = forms.Fields.Name,
                                Surname = forms.Fields.Surname,
                                Age = forms.Fields.Age
                            }
                        };
                        return Json(form, JsonRequestBehavior.AllowGet);
                    }
                    return Json("notfound", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = db.Forms.ToList();
                    var forms = new List<object>();
                    foreach (var i in result)
                    {
                        forms.Add(new Form()
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Description = i.Description,
                            CreatedAt = i.CreatedAt,
                            CreatedBy = i.Users.Id,
                            Fields = new Field()
                            {
                                Name = i.Fields.Name,
                                Surname = i.Fields.Surname,
                                Age = i.Fields.Age
                            },
                            Users = new User()
                            {
                                Username = i.Users.Username
                            }
                        });
                    }
                    return Json(forms, JsonRequestBehavior.AllowGet);
                }
            }
            return Json("unauthorized", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult forms(Form formModel)
        {
            if (Session["userId"] != null)
            {
                var userIds =  (int) Session["userId"];
                if (formModel != null)
                {
                    //db = new DataContext();
                    Field field = new Field()
                    {
                        Name = formModel.Fields.Name,
                        Surname = formModel.Fields.Surname,
                        Age = formModel.Fields.Age
                    };
                    db.Fields.Add(field);
                    db.SaveChanges();

                    Form form = new Form()
                    {
                        Name = formModel.Name,
                        Description = formModel.Description,
                        CreatedAt = DateTime.Now.Date,
                        CreatedBy = userIds,
                        Field_Id = field.Id
                    };
                    db.Forms.Add(form);
                    db.SaveChanges();
                    return Json(1);
                }
                return Json(-1);
            }
            return Json(0);
        }






        public ActionResult Login()
        {

            return View();
        }

        public JsonResult CheckLogin(User model)
        {
            string result = "Fail";

            var DataItem = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password).SingleOrDefault();

            if (DataItem != null)
            {
                Session["userId"] = DataItem.Id;
                Session["username"] = DataItem.Username.ToString();
                result = "success";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}