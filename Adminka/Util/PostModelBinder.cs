﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Blog.Core.Models;
using Blog.Core.Stores;

namespace Adminka.Util
{
    public class PostModelBinder : DefaultModelBinder
    {
        private readonly IBlogRepository _blogRepository;

        public PostModelBinder(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var post = (Post)base.BindModel(controllerContext, bindingContext);

            if (post.Category != null)
                post.Category = _blogRepository.Category(post.Category.Id);

            var tags = bindingContext.ValueProvider.GetValue("Tags").AttemptedValue.Split(',');

            if (tags.Length > 0)
            {
                post.Tags = new List<Tag>();

                foreach (var tag in tags)
                {
                    post.Tags.Add(_blogRepository.Tag(int.Parse(tag.Trim())));
                }
            }

            if (bindingContext.ValueProvider.GetValue("oper").AttemptedValue.Equals("edit"))
                post.Modified = DateTime.UtcNow;
            else
                post.PostedOn = DateTime.UtcNow;

            return post;
        }
    }
}