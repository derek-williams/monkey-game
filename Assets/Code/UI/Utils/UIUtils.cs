using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

	public static class UIUtils
	{
		public static void BringToFront(GameObject inObject)
		{
			Canvas root = UIUtils.FindInParents<Canvas>(inObject);
			if (root != null)
				inObject.transform.SetParent(root.transform, true);

			inObject.transform.SetAsLastSibling();
		}

		public static T FindInParents<T>(GameObject inObject) where T : Component
		{
			if (inObject == null)
				return null;

			var comp = inObject.GetComponent<T>();
			if (comp != null)
				return comp;

			Transform t = inObject.transform.parent;

			while (t != null && comp == null)
			{
				comp = t.gameObject.GetComponent<T>();
				t = t.parent;
			}
            return comp;
		}


		public static CompType GetChildOfTypeWithName<CompType>(GameObject go, string compName) where CompType : Component
		{
			// if we have a game object
			if (null != go)
			{
				CompType[] subComps = go.GetComponentsInChildren<CompType>(true);
				return System.Array.Find<CompType>(subComps, new System.Predicate<CompType>(x => x.name == compName));
			}

			return null;
		}
	}