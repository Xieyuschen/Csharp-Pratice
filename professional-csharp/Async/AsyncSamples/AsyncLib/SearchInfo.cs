using System.Collections.ObjectModel;

namespace Wrox.ProCSharp.Async
{
  public class SearchInfo : BindableBase
  {
      public SearchInfo()
      {
            _list = new ObservableCollection<SearchItemResult>();
            //_list为ObservableCollection<SearchItemResult>类型的，在添加的时候会进行提示
            //调用基类的一个方法，一会有空来看一下
            _list.CollectionChanged += delegate { OnPropertyChanged("List"); };
      }

    private string _searchTerm;

    public string SearchTerm
    {
      get { return _searchTerm; }
      set { SetProperty(ref _searchTerm, value); }
    }

    private ObservableCollection<SearchItemResult> _list;
    //List属性用来返回返回所有找到图片的列表，每个图片用一个SearchItemResult对象表示

    //表示一个动态数据集合，它可在添加、删除项目或刷新整个列表时提供通知。
    public ObservableCollection<SearchItemResult> List => _list;

  } 
}
