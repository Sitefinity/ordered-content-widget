Ordered Content Widget
-----------------------

### Installation Instructions

1. Clone the **ordered-content-widget** repository to your machine
2. Open your Sitefinity website in Visual Studio
3. Add **Telerik.Sitefinity.OrderedContentWidget** (that you've cloned in first step) project to your solution
4. Fix all the references in the **Telerik.Sitefinity.OrderedContentWidget** project to point to the bin folder of your website in order to build the widget for your exact version of Sitefinity. The references that need to be fixed are:
  * ServiceStack
  * ServiceStack.Common
  * ServiceStack.Interfaces
  * ServiceStack.Text
  * Telerik.OpenAccess
  * Telerik.Sitefinity
  * Telerik.Sitefinity.Model
  * Telerik.Sitefinity.Utilities
  * Telerik.Web.UI
5. Now, reference the **Telerik.Sitefinity.OrderedContentWidget** project from your Sitefinity project (usually named SitefinityWebApp)
6. Build solution
7. Open Sitefinity administration in the browser
8. Navigate to **Administration** > **Settings** > **Advanced**
9. In the left pane, locate following item: **System** > **ApplicationModules** and then click on **Create new**
10. Fill in the *Create new application module form* as follows:
  * Name : **OrderedContentWidget**
  * Title : **Ordered Content Widget**
  * Description : **Widget which allows arbitrary ordering of content items**
  * Type : **Telerik.Sitefinity.OrderedContentWidget.Module, Telerik.Sitefinity.OrderedContentWidget**
  * StartupType : **OnApplicationStart**
11. Click **Save changes**
12. In Sitefinity navigate to **Administration** > **Modules & Services **
13. Locate the **Ordered Content Widget** module, click on the **Actions** and from the menu click on the **Install** in case module is not active already

You are now ready. You will find a new widget in your toolbox called **Ordered Content** which you can use to display arbitrary content items sorted in a particular way.
