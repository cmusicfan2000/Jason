﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace Jason.Models {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class WorshipServiceOrder {
        
        private object[] itemsField;
        
        private string themeColorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("FamilyNewsAndPrayer", typeof(FamilyNewsAndPrayer))]
        [System.Xml.Serialization.XmlElementAttribute("LordsSupper", typeof(LordsSupper))]
        [System.Xml.Serialization.XmlElementAttribute("Placeholder", typeof(Placeholder))]
        [System.Xml.Serialization.XmlElementAttribute("Scripture", typeof(Scripture))]
        [System.Xml.Serialization.XmlElementAttribute("Sermon", typeof(Sermon))]
        [System.Xml.Serialization.XmlElementAttribute("Song", typeof(Song))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ThemeColor {
            get {
                return this.themeColorField;
            }
            set {
                this.themeColorField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FamilyNewsAndPrayer {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Sermon {
        
        private string titleField;
        
        private string presenterField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Presenter {
            get {
                return this.presenterField;
            }
            set {
                this.presenterField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LordsSupper {
        
        private Scripture scriptureField;
        
        /// <remarks/>
        public Scripture Scripture {
            get {
                return this.scriptureField;
            }
            set {
                this.scriptureField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Scripture : ImageBackground {
        
        private Translation itemField;
        
        private ScriptureBook bookField;
        
        private string referenceField;
        
        private string textField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Translation")]
        public Translation Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ScriptureBook Book {
            get {
                return this.bookField;
            }
            set {
                this.bookField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Reference {
            get {
                return this.referenceField;
            }
            set {
                this.referenceField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Text {
            get {
                return this.textField;
            }
            set {
                this.textField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Translation {
        
        private string abbreviationField;
        
        private string fullNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Abbreviation {
            get {
                return this.abbreviationField;
            }
            set {
                this.abbreviationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string FullName {
            get {
                return this.fullNameField;
            }
            set {
                this.fullNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public enum ScriptureBook {
        
        /// <remarks/>
        Genesis,
        
        /// <remarks/>
        Exodus,
        
        /// <remarks/>
        Leviticus,
        
        /// <remarks/>
        Numbers,
        
        /// <remarks/>
        Deuteronomy,
        
        /// <remarks/>
        Joshua,
        
        /// <remarks/>
        Judges,
        
        /// <remarks/>
        Ruth,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Samuel")]
        Item1Samuel,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Samuel")]
        Item2Samuel,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Kings")]
        Item1Kings,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Kings")]
        Item2Kings,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Chronicles")]
        Item1Chronicles,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Chronicles")]
        Item2Chronicles,
        
        /// <remarks/>
        Ezra,
        
        /// <remarks/>
        Nehemiah,
        
        /// <remarks/>
        Esther,
        
        /// <remarks/>
        Job,
        
        /// <remarks/>
        Psalms,
        
        /// <remarks/>
        Proverbs,
        
        /// <remarks/>
        Ecclesiastes,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("Song of Solomon")]
        SongofSolomon,
        
        /// <remarks/>
        Isaiah,
        
        /// <remarks/>
        Jeremiah,
        
        /// <remarks/>
        Lamentations,
        
        /// <remarks/>
        Ezekiel,
        
        /// <remarks/>
        Daniel,
        
        /// <remarks/>
        Hosea,
        
        /// <remarks/>
        Joel,
        
        /// <remarks/>
        Amos,
        
        /// <remarks/>
        Obadiah,
        
        /// <remarks/>
        Jonah,
        
        /// <remarks/>
        Micah,
        
        /// <remarks/>
        Nahum,
        
        /// <remarks/>
        Habakkuk,
        
        /// <remarks/>
        Zephaniah,
        
        /// <remarks/>
        Haggai,
        
        /// <remarks/>
        Zechariah,
        
        /// <remarks/>
        Malachi,
        
        /// <remarks/>
        Matthew,
        
        /// <remarks/>
        Mark,
        
        /// <remarks/>
        Luke,
        
        /// <remarks/>
        John,
        
        /// <remarks/>
        Acts,
        
        /// <remarks/>
        Romans,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Corinthians")]
        Item1Corinthians,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Corinthians")]
        Item2Corinthians,
        
        /// <remarks/>
        Galatians,
        
        /// <remarks/>
        Ephesians,
        
        /// <remarks/>
        Philippians,
        
        /// <remarks/>
        Colossians,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Thessalonians")]
        Item1Thessalonians,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Thessalonians")]
        Item2Thessalonians,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Timothy")]
        Item1Timothy,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Timothy")]
        Item2Timothy,
        
        /// <remarks/>
        Titus,
        
        /// <remarks/>
        Philemon,
        
        /// <remarks/>
        Hebrews,
        
        /// <remarks/>
        James,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 Peter")]
        Item1Peter,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 Peter")]
        Item2Peter,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1 John")]
        Item1John,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2 John")]
        Item2John,
        
        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("3 John")]
        Item3John,
        
        /// <remarks/>
        Jude,
        
        /// <remarks/>
        Revelation,
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Scripture))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Placeholder))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImageBackground {
        
        private string backgroundImageNameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string BackgroundImageName {
            get {
                return this.backgroundImageNameField;
            }
            set {
                this.backgroundImageNameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Placeholder : ImageBackground {
        
        private string nameField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SongPart {
        
        private string slidesField;
        
        private string nameField;
        
        /// <remarks/>
        public string Slides {
            get {
                return this.slidesField;
            }
            set {
                this.slidesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Song {
        
        private SongPart[] partField;
        
        private string titleField;
        
        private string slideshowField;
        
        private ushort bookNumberField;
        
        private bool bookNumberFieldSpecified;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Part")]
        public SongPart[] Part {
            get {
                return this.partField;
            }
            set {
                this.partField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Title {
            get {
                return this.titleField;
            }
            set {
                this.titleField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Slideshow {
            get {
                return this.slideshowField;
            }
            set {
                this.slideshowField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public ushort BookNumber {
            get {
                return this.bookNumberField;
            }
            set {
                this.bookNumberField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BookNumberSpecified {
            get {
                return this.bookNumberFieldSpecified;
            }
            set {
                this.bookNumberFieldSpecified = value;
            }
        }
    }
}
