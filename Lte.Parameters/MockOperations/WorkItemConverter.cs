using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using Lte.Parameters.Entities.Work;

namespace Lte.Parameters.MockOperations
{
    public class WorkItemConverter : TypeConverter<WorkItemExcel, WorkItem>
    {
        protected override WorkItem ConvertCore(WorkItemExcel source)
        {
            var result = new WorkItem();
            source.CloneProperties(result);
            result.Cause = source.CauseDescription.GetWorkItemCause();
            result.State = source.StateDescription.GetState();

            var title = source.Title ?? "";
            var typeFields = title.Split('_');
            var titleFields = title.GetSplittedFields("--");
            var titleFields2 = title.GetSplittedFields("—");
            if (typeFields.Length > 3)
            {
                result.Type = typeFields[1].GetWorkItemType();
                result.Subtype = typeFields[2].GetWorkItemSubtype();
                result.FeedbackContents = "[" + DateTime.Now + "]创建信息：" + typeFields[3];
            }
            else if (typeFields.Length == 3)
            {
                result.Type = typeFields[1].GetWorkItemType();
                result.Subtype = WorkItemSubtype.Others;
                result.FeedbackContents = "[" + DateTime.Now + "]创建信息：" + typeFields[2];
            }
            else if (titleFields.Length == 2)
            {
                result.Type = titleFields[0].GetWorkItemType();
                result.Subtype = WorkItemSubtype.Others;
                result.FeedbackContents = "[" + DateTime.Now + "]创建信息：" + titleFields[1];
            }
            else if (titleFields2.Length == 2)
            {
                result.Type = titleFields2[0].GetWorkItemType();
                result.Subtype = WorkItemSubtype.Others;
                result.FeedbackContents = "[" + DateTime.Now + "]创建信息：" + titleFields2[1];
            }
            else
            {
                result.Type = WorkItemType.Others;
                result.Subtype = WorkItemSubtype.Others;
                result.FeedbackContents = "[" + DateTime.Now + "]创建信息：" + title;
            }
            return result;
        }
    }
}
