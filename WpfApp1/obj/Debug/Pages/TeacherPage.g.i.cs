﻿#pragma checksum "..\..\..\Pages\TeacherPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A67DC307F5B6F719BFBFC62C66D0F5BCEBA18D0D5F412998018E1B740FCDD476"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp1.Pages;


namespace WpfApp1.Pages {
    
    
    /// <summary>
    /// TeacherPage
    /// </summary>
    public partial class TeacherPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ColumnDefinition ChangeColumn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddTeacher;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CopyTeacher;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditTeacher;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DeleteTeacher;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TeacherDataGrid;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TeacherLabel;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextSurnameTeacher;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextNameTeacher;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextPatronymicTeacher;
        
        #line default
        #line hidden
        
        
        #line 86 "..\..\..\Pages\TeacherPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TeacherAddCommit;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/pages/teacherpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\TeacherPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ChangeColumn = ((System.Windows.Controls.ColumnDefinition)(target));
            return;
            case 2:
            this.AddTeacher = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\TeacherPage.xaml"
            this.AddTeacher.Click += new System.Windows.RoutedEventHandler(this.ClickAddTeacher);
            
            #line default
            #line hidden
            return;
            case 3:
            this.CopyTeacher = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Pages\TeacherPage.xaml"
            this.CopyTeacher.Click += new System.Windows.RoutedEventHandler(this.ClickCopyTeacher);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EditTeacher = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Pages\TeacherPage.xaml"
            this.EditTeacher.Click += new System.Windows.RoutedEventHandler(this.ClickEditTeacher);
            
            #line default
            #line hidden
            return;
            case 5:
            this.DeleteTeacher = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Pages\TeacherPage.xaml"
            this.DeleteTeacher.Click += new System.Windows.RoutedEventHandler(this.ClickDeleteTeacher);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TeacherDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            
            #line 71 "..\..\..\Pages\TeacherPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TeacherAddRollback_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.TeacherLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.TextSurnameTeacher = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.TextNameTeacher = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.TextPatronymicTeacher = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.TeacherAddCommit = ((System.Windows.Controls.Button)(target));
            
            #line 86 "..\..\..\Pages\TeacherPage.xaml"
            this.TeacherAddCommit.Click += new System.Windows.RoutedEventHandler(this.TeacherAddCommit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
