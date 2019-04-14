================================================================================
    MICROSOFT 基础类库 : NetAdjustment 项目概述
===============================================================================

应用程序向导已为您创建了此 NetAdjustment 应用程序。此应用程序不仅演示 Microsoft 基础类的基本使用方法，还可作为您编写应用程序的起点。


/////////////////////////////////////////////////////////////////////////////
计算有关文件:
DataCenter.h       用于定义本程序所有用到的类型
CoordCalculate.cpp 包含所有与坐标转换坐标计算有关函数
NetAdjustment.cpp  包含所有与平差计算有关的函数

/////////////////////////////////////////////////////////////////////////////
其他注释:

1.程序的输入文件为根目录下的static文件夹中;
2.文件格式由于中文字符的编码不一致性，采用了固定跳过行数的方法暂时跳过,
  若有需要可自行添加行标志进行选择性读取
3.程序采用Eigen库函数编写,有特殊需要可自行编写矩阵函数
4.程序设置了无需预编译处理以及多字符集
5.程序计算有关的三个文件耦合性很低，可自行取用

/////////////////////////////////////////////////////////////////////////////


本文件概要介绍组成 NetAdjustment 应用程序的每个文件的内容。

NetAdjustment.vcxproj
    这是使用应用程序向导生成的 VC++ 项目的主项目文件，其中包含生成该文件的 Visual C++ 的版本信息，以及有关使用应用程序向导选择的平台、配置和项目功能的信息。

NetAdjustment.vcxproj.filters
    这是使用“应用程序向导”生成的 VC++ 项目筛选器文件。它包含有关项目文件与筛选器之间的关联信息。在 IDE 中，通过这种关联，在特定节点下以分组形式显示具有相似扩展名的文件。例如，“.cpp”文件与“源文件”筛选器关联。

NetAdjustment.h
    这是应用程序的主头文件。
    其中包括其他项目特定的标头（包括 Resource.h），并声明 CNetAdjustmentApp 应用程序类。

NetAdjustment.cpp
    这是包含应用程序类 CNetAdjustmentApp 的主应用程序源文件。

NetAdjustment.rc
    这是程序使用的所有 Microsoft Windows 资源的列表。它包括 RES 子目录中存储的图标、位图和光标。此文件可以直接在 Microsoft Visual C++ 中进行编辑。项目资源包含在 2052 中。

res\NetAdjustment.ico
    这是用作应用程序图标的图标文件。此图标包括在主资源文件 NetAdjustment.rc 中。

res\NetAdjustment.rc2
    此文件包含不在 Microsoft Visual C++ 中进行编辑的资源。您应该将不可由资源编辑器编辑的所有资源放在此文件中。

NetAdjustment.reg
    这是一个示例 .reg 文件，显示框架将为您设置的注册设置类型。可以将此作为 .reg
    文件随应用程序一起使用，或直接删除该文件并使用默认的 RegisterShellFileTypes 注册。


/////////////////////////////////////////////////////////////////////////////

对于主框架窗口：
    该项目包含一个标准的 MFC 接口。

MainFrm.h, MainFrm.cpp
    这些文件中包含框架类 CMainFrame，该类派生自
    CFrameWnd 并控制所有 SDI 框架功能。

res\Toolbar.bmp
    此位图文件用于为工具栏创建平铺图像。
    初始工具栏和状态栏在 CMainFrame 类中构造。使用资源编辑器编辑此工具栏位图，并更新 NetAdjustment.rc 中的 IDR_MAINFRAME TOOLBAR 数组以添加工具栏按钮。
/////////////////////////////////////////////////////////////////////////////

应用程序向导创建一种文档类型和一个视图：

NetAdjustmentDoc.h、NetAdjustmentDoc.cpp - 文档
    这些文件包含 CNetAdjustmentDoc 类。编辑这些文件以添加特殊文档数据并实现文件保存和加载（通过 CNetAdjustmentDoc::Serialize）。
    该文档将包含以下字符串：
        文件扩展名：      NetAdjustment
        文件类型 ID：        NetAdjustment.Document
        主框架标题：  NetAdjustment
        文档类型名称：       NetAdjustment
        筛选器名称：         NetAdjustment Files (*.NetAdjustment)
        文件的新短名称：NetAdjustment
        文件类型长名称：NetAdjustment.Document

NetAdjustmentView.h、NetAdjustmentView.cpp - 文档视图
    这些文件包含 CNetAdjustmentView 类。
    CNetAdjustmentView 对象用于查看 CNetAdjustmentDoc 对象。




/////////////////////////////////////////////////////////////////////////////

其他功能：

ActiveX 控件
    该应用程序包含对使用 ActiveX 控件的支持。

/////////////////////////////////////////////////////////////////////////////

其他标准文件:

StdAfx.h, StdAfx.cpp
    这些文件用于生成名为 NetAdjustment.pch 的预编译头 (PCH) 文件和名为 StdAfx.obj 的预编译类型文件。

Resource.h
    这是标准头文件，可用于定义新的资源 ID。Microsoft Visual C++ 将读取并更新此文件。

NetAdjustment.manifest
	Windows XP 使用应用程序清单文件来描述特定版本的并行程序集的应用程序依赖项。加载程序使用这些信息来从程序集缓存中加载相应的程序集，并保护其不被应用程序访问。应用程序清单可能会包含在内，以作为与应用程序可执行文件安装在同一文件夹中的外部 .manifest 文件进行重新分发，它还可能以资源的形式包含在可执行文件中。
/////////////////////////////////////////////////////////////////////////////

其他注释:

应用程序向导使用“TODO:”来指示应添加或自定义的源代码部分。

如果应用程序使用共享 DLL 中的 MFC，您将需要重新分发 MFC DLL。如果应用程序所使用的语言与操作系统的区域设置不同，则还需要重新分发相应的本地化资源 mfc110XXX.DLL。
有关上述话题的更多信息，请参见 MSDN 文档中有关重新分发 Visual C++ 应用程序的部分。

/////////////////////////////////////////////////////////////////////////////
