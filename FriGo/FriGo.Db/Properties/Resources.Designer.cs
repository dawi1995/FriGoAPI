﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FriGo.Db.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FriGo.Db.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The new password and confirmation password do not match..
        /// </summary>
        internal static string ConfirmPasswordValidationMessage {
            get {
                return ResourceManager.GetString("ConfirmPasswordValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pole {0} nie może być puste..
        /// </summary>
        internal static string EmptyGenericValidationMessage {
            get {
                return ResourceManager.GetString("EmptyGenericValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Jajko
        ///Ryż.
        /// </summary>
        internal static string IngredientList {
            get {
                return ResourceManager.GetString("IngredientList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nazwa składnika nie może być pusta..
        /// </summary>
        internal static string IngredientNameEmptyValidationMessage {
            get {
                return ResourceManager.GetString("IngredientNameEmptyValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Nazwa składnika musi mieścić się w granicach 1-100 znaków..
        /// </summary>
        internal static string IngredientNameLengthValidationMessage {
            get {
                return ResourceManager.GetString("IngredientNameLengthValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Mamy już składnik o takiej nazwie..
        /// </summary>
        internal static string IngredientNameUniqueValidationMessage {
            get {
                return ResourceManager.GetString("IngredientNameUniqueValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ilość musi być liczbą dodatnią..
        /// </summary>
        internal static string IngredientQuantityValidationMessage {
            get {
                return ResourceManager.GetString("IngredientQuantityValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Jednostka o podanym Id nie istnieje..
        /// </summary>
        internal static string IngredientUnitExistMessage {
            get {
                return ResourceManager.GetString("IngredientUnitExistMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password must be at least {0} characters long..
        /// </summary>
        internal static string PasswordLengthValidationMessage {
            get {
                return ResourceManager.GetString("PasswordLengthValidationMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to gram
        ///mililitr.
        /// </summary>
        internal static string UnitList {
            get {
                return ResourceManager.GetString("UnitList", resourceCulture);
            }
        }
    }
}
