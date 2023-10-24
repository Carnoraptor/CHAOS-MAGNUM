using Godot;
using System;

namespace Utility
{
	public partial class NodeUtils : Node2D
	{
		//Thanks ChatGPT :)
		public T FindNode<T>(Node parent) where T : Node
		{
			foreach (Node child in parent.GetChildren())
			{
				if (child is T typedNode)
				{
					return typedNode;
				}

				T foundNode = FindNode<T>(child);
				if (foundNode != null)
				{
					return foundNode;
				}
			}
			return null;
		}
	}
}
