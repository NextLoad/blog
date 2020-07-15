using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nextload.Blog
{
    public class SwaggerDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var tags = new List<OpenApiTag>
            {
                new OpenApiTag
                {
                    Name = "Post",
                    Description = "个人博客相关接口",
                    ExternalDocs = new OpenApiExternalDocs{Description = "包含：文章/标签/分类/友链" }
                },
                new OpenApiTag
                {
                    Name = "Category",
                    Description = "个人博客目录相关接口"
                },
            };

            swaggerDoc.Tags = tags.OrderBy(x=>x.Name).ToList();
        }
    }
}
