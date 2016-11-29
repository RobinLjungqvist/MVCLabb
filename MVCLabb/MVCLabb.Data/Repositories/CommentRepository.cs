using MVCLabb.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCLabb.Data.Models;

namespace MVCLabb.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public bool AddOrUpdate(CommentEntityModel comment)
        {
            try
            {
                using (var ctx = new DataContext())
                {
                    var commentToUpdate = ctx.Comments.Find(comment.id);
                    if (commentToUpdate != null)
                    {
                        commentToUpdate.Title = comment.Title;
                        commentToUpdate.Comment = comment.Comment;
                        commentToUpdate.DateEdited = comment.DateEdited;
                        ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        var newComment = new CommentEntityModel();
                        newComment.Title = comment.Title;
                        newComment.Comment = comment.Comment;
                        newComment.DateEdited = comment.DateEdited;
                        newComment.UserID = comment.UserID;
                        newComment.PictureID = comment.PictureID;
                        ctx.Comments.Add(newComment);
                        ctx.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                //Handle exceptions
            }
            return false;
        }

        public IEnumerable<CommentEntityModel> All()
        {
            using (var ctx = new DataContext())
            {
                var comment = ctx.Comments;
                return comment;
            }
        }

        public CommentEntityModel ByID(int id)
        {
            using (var ctx = new DataContext())
            {
                var comment = ctx.Comments.Find(id);
                return comment;
            }
        }

        public bool Delete(int id)
        {
            using (var ctx = new DataContext())
            {
                var comment = ctx.Comments.Find(id);
                if (comment != null)
                {
                    ctx.Comments.Remove(comment);
                    ctx.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
