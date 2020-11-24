using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BizzyBeeGames.WordGame
{
	public class UIScreenCategoryLevels : UIScreen
	{
		private class CategoryInfoMQ
		{
			public string name;                       // Name for the category, used in filename, should be unique
			public string displayName;                // Name that is displayed in the UI
			public string description;                // Short description, can be anything
			public Sprite icon;                       // An icon that goes with the category
			public List<LevelInfoMQ> levelInfos;      // The list of levels in this category
		}

		private class LevelInfoMQ
		{
			public string[] words = null;
		}


		//private List<LevelInfo> CategoryInfosMegaquiz = { "CATEGORIA A" };

		private string[,] wordsMegaquiz = new string[,] {
															{ "ABC", "DEF", "GHI" },
															{ "123", "456", "789" },
															{ "JESUS", "DEUS", "SENHOR" }
														};

		#region Inspector Variables

		[Space]

		[SerializeField] private Transform levelListContainer;
		[SerializeField] private LevelListItem levelListItemPrefab;

		#endregion

		#region Member Variables

		private ObjectPool levelItemObjectPool;

		#endregion

		#region Public Methods

		public override void Initialize()
		{
			base.Initialize();

			levelItemObjectPool = new ObjectPool(levelListItemPrefab.gameObject, 10, levelListContainer);
		}

		public override void OnShowing(object data)
		{
			base.OnShowing(data);

			levelItemObjectPool.ReturnAllObjectsToPool();

			CategoryInfo categoryInfo = GameManager.Instance.GetCategoryInfo((string)data);
			bool completed = true;

			//=====> ESTOU MEXENDO AQUI
			//categoryInfo.name = "nome";
			categoryInfo.displayName = "display";
			categoryInfo.description = "descricao";
			categoryInfo.icon = Resources.Load<Sprite>("Resources/WordGamesSprites/" + "sports_icon.png");

			//string[] vetor = new string[] { "EU", "TU", "ELE" };

			//for (int i = 0; i < categoryInfo.levelInfos.Count; i++)
			//{
				categoryInfo.levelInfos[0].words = new string[] { "EU", "TU", "ELE" };
			//}


			//categoryInfo = category;

			//<===== ATÉ AQUI

			for (int i = 0; i < categoryInfo.levelInfos.Count; i++)
			{
				LevelListItem.Type type = completed ? LevelListItem.Type.Completed : LevelListItem.Type.Locked;

				if (completed && !GameManager.Instance.IsLevelCompleted(categoryInfo, i))
				{
					completed	= false;
					type		= LevelListItem.Type.Normal;
				}
				
				LevelListItem levelListItem = levelItemObjectPool.GetObject().GetComponent<LevelListItem>();

				levelListItem.Setup(categoryInfo, i, type);
				levelListItem.gameObject.SetActive(true);
			}
		}
		
		public void OnBackClicked()
		{
			// Go back to main screen
			UIScreenController.Instance.Show(UIScreenController.CategoriesScreenId, true);
		}
		
		#endregion
	}
}
