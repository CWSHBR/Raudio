﻿#pragma checksum "..\..\..\musicFile.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8B6C4311E2B0544D8DDD82C1E23DE4982A9CD9B5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Raudio;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace Raudio {
    
    
    /// <summary>
    /// musicFile
    /// </summary>
    public partial class musicFile : System.Windows.Controls.Grid, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\musicFile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid track;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\musicFile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label titleArtist;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\musicFile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label duration;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\musicFile.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Path;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Raudio;component/musicfile.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\musicFile.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.track = ((System.Windows.Controls.Grid)(target));
            
            #line 9 "..\..\..\musicFile.xaml"
            this.track.MouseEnter += new System.Windows.Input.MouseEventHandler(this.mouseEnter);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\musicFile.xaml"
            this.track.MouseLeave += new System.Windows.Input.MouseEventHandler(this.mouseLeave);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\musicFile.xaml"
            this.track.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.cliked);
            
            #line default
            #line hidden
            return;
            case 2:
            this.titleArtist = ((System.Windows.Controls.Label)(target));
            
            #line 15 "..\..\..\musicFile.xaml"
            this.titleArtist.MouseEnter += new System.Windows.Input.MouseEventHandler(this.longLabelEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\..\musicFile.xaml"
            this.titleArtist.MouseLeave += new System.Windows.Input.MouseEventHandler(this.longLabelLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.duration = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.Path = ((System.Windows.Controls.Label)(target));
            
            #line 35 "..\..\..\musicFile.xaml"
            this.Path.MouseEnter += new System.Windows.Input.MouseEventHandler(this.longLabelEnter);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\musicFile.xaml"
            this.Path.MouseLeave += new System.Windows.Input.MouseEventHandler(this.longLabelLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

