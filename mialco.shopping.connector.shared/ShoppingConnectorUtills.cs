using System;
using System.Collections.Generic;
using System.Text;

namespace mialco.shopping.connector.shared
{
	public class ShoppingConnectorUtills
	{

		public static WebStoreDeploymentType DeploymentTypeFromString(string deploymentType)
		{
			WebStoreDeploymentType result;
			deploymentType = (deploymentType ?? string.Empty).ToLower();
			switch (deploymentType)
			{
				case "production":
					result = WebStoreDeploymentType.Production;
					break;
				case "staging":
					result = WebStoreDeploymentType.Staging;
					break;
				case "development":
					result = WebStoreDeploymentType.Development;
					break;
				default:
					result = WebStoreDeploymentType.Undefined;
					break;
			}

			return result;
		}

	}
}
