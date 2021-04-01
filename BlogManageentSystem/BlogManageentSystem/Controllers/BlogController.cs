using BlogManageentSystem.Data;
using BlogManageentSystem.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogManageentSystem.Controllers
{
    public class BlogController : ApiController
    {
        DatabaseContext db = new DatabaseContext();

        //api/blog
        public IEnumerable<Blog> GetBlogs()
        {
            return db.Blogs.ToList();
        }

        //api/blog/2
        public Blog GetBlog(int id)
        {
            return db.Blogs.Find(id);
        }

        //api/blog
        [HttpPost]
        public HttpResponseMessage AddBlog(Blog model)
        {
            try
            {
                model.DateOfUpload = DateTime.Today;
                db.Blogs.Add(model);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //api/blog/2
        [HttpPut]
        public HttpResponseMessage UpdateBlog(int id,Blog model)
        {
            try
            {
                if(id == model.BlogId)
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    return response;
                }
                else
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotModified);
                    return response;
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                return response;
            }
        }

        //api/blog/2
        public HttpResponseMessage DeleteBlog(int id)
        {
            Blog blog = db.Blogs.Find(id);
            if(blog != null)
            {
                db.Blogs.Remove(blog);
                db.SaveChanges();
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                return response;
            }
            else
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.NotFound);
                return response;
            }
        }
    }
}
