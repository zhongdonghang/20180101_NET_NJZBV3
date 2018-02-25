using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManageWebV2.Code
{
    public class ReadingRoomTreeNodes
    {
        /// <summary>
        /// 获取阅览室节点
        /// </summary>
        /// <returns></returns>
        public static List<FineUI.TreeNode> GetReadingRoomTreeNodes(string loginId)
        { 
            SeatManage.ClassModel.ManagerPotency mp = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginId);
            List<FineUI.TreeNode> treeNodes = new List<FineUI.TreeNode>();
            if (mp != null)
            {
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in mp.RightRoomList)
                {
                    bool isExists = false;
                    for (int i = 0; i < treeNodes.Count; i++)//遍历已经存在的一级节点，判断所管理的阅览室是否存在。
                    {
                        if (treeNodes[i].NodeID == room.Libaray.No)
                        { //相同，说明已经存在 
                            FineUI.TreeNode node1 = new FineUI.TreeNode();//构造一个二级节点，添加到一级节点下。
                            node1.EnablePostBack = true;
                            node1.Text = room.Name;
                            node1.NodeID = room.No;
                            node1.ToolTip = room.No; 
                            treeNodes[i].Nodes.Add(node1);
                            isExists = true;
                            break;
                        }
                    }
                    if (!isExists)
                    {
                        FineUI.TreeNode node0 = new FineUI.TreeNode();
                        node0.Expanded = true;
                        node0.Text = room.Libaray.Name;
                        node0.NodeID = room.Libaray.No;
                        node0.ToolTip = room.Libaray.No;
                        FineUI.TreeNode node1 = new FineUI.TreeNode();
                        node1.Text = room.Name;
                        node1.EnablePostBack = true;
                        node1.NodeID = room.No;
                        node1.ToolTip = room.No;
                        node0.Nodes.Add(node1);
                        treeNodes.Add(node0);
                    }
                }
            }
            return treeNodes;


        }
    }
}