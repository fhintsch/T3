# T3
Test of CollectionView with Observable Collection

The test shows a searchfield that searches for static content defined in the viewmodel _SearchViewModel.cs_ . The result should be displayed with a grouped _CollectionView_ .
Intermediate results are shown at the application output window.

If you use _ObservableCollection_ it won't work. Correct results you will get with _ObservableRangeCollection_.

## Test with ObservableCollection
You must use _SearchViewModel.cs_. This module is enabled in _Mainpage.xaml_ line 5. Substitute "xxx" by "ViewModels". 
This will not work. It will produce 
```
*** Assertion failure in -[UICollectionView _Bug_Detected_In_Client_Of_UICollectionView_Invalid_Batch_Updates:], UICollectionView.m:10064
```

## Test with ObservableRangeCollection
You must use _SearchViewModel.cs_. This module is enabled in _Mainpage.xaml_ line 6. Substitute "xxx" by "ViewModels".

## Environment
- Visual Studio 2022 for Mac
- .Net 7 MAUI
- Target: MacCatalyst on a Macbook M2






