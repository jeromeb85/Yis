using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yis.Designer.Software.Business;
using Yis.Designer.Technic.Contract;
using Yis.Framework.Core.Extension;

namespace Yis.Designer.Technic.Internal
{
    public class Generator : IGenerator
    {
        #region Fields

        private CSharpCodeProvider _codeProvider;

        #endregion Fields

        //private CodeCompileUnit _compileUnit;

        #region Properties

        public CSharpCodeProvider CodeProvider
        {
            get
            {
                if (_codeProvider.IsNull())
                {
                    _codeProvider = new CSharpCodeProvider();
                }
                return _codeProvider;
            }
        }

        #endregion Properties

        //public CodeCompileUnit CompileUnit
        //{
        //    get
        //    {
        //        if (_compileUnit.IsNull())
        //        {
        //            _compileUnit = new CodeCompileUnit();
        //        }
        //        return _compileUnit;
        //    }
        //}

        #region Methods

        public void Generate(NameSpace root, string outputDirectory)
        {
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            GenerateWithCodeDom(root, outputDirectory);
            //CodeProvider.

            //StringBuilder generatedCode = new StringBuilder();
            //StringWriter codeWriter = new StringWriter(generatedCode);
            //CodeGeneratorOptions options = new CodeGeneratorOptions();
            //options.
            //options.BracingStyle = "C";
            //CodeGenerator.GenerateCodeFromCompileUnit(CompileUnit, codeWriter, options);
            //Console.WriteLine(generatedCode.ToString());
        }

        private void GenerateWithCodeDom(NameSpace param, string outputDirectory, string parent = null)
        {
            if (!Directory.Exists(outputDirectory + @"\" + param.Name))
                Directory.CreateDirectory(outputDirectory + @"\" + param.Name);

            if (param.Class.Count() != 0)
            {
                param.Class.ForEach((i) => GenerateWithCodeDom(i, outputDirectory + @"\" + param.Name, string.IsNullOrEmpty(parent) ? param.Name : parent + "." + param.Name));
            }

            param.Sub.ForEach((i) => GenerateWithCodeDom(i, outputDirectory + @"\" + param.Name, string.IsNullOrEmpty(parent) ? param.Name : parent + "." + param.Name));
        }

        private void GenerateWithCodeDom(Class param, string outputDirectory, string nameSpace)
        {
            CodeCompileUnit unit = new CodeCompileUnit();

            CodeNamespace ns = new CodeNamespace(nameSpace);

            param.Import.ForEach((s) => ns.Imports.Add(new CodeNamespaceImport(s)));

            CodeTypeDeclaration ctd = new CodeTypeDeclaration(param.Name);
            ctd.IsClass = true;
            ctd.Attributes = MemberAttributes.Public;

            if (!string.IsNullOrWhiteSpace(param.BaseType)) ctd.BaseTypes.Add(param.BaseType);
            param.Implement.ForEach((i) => ctd.BaseTypes.Add(i));
            param.Property.ForEach((i) => GenerateWithCodeDom(i, ctd));

            //Génération de la classe
            ns.Types.Add(ctd);

            unit.Namespaces.Add(ns);

            using (StreamWriter sw = new StreamWriter(outputDirectory + @"\" + param.Name + "." + CodeProvider.FileExtension, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                CodeProvider.GenerateCodeFromCompileUnit(unit, tw, new CodeGeneratorOptions());
                tw.Close();
            }
        }

        private void GenerateWithCodeDom(Property param, CodeTypeDeclaration parent)
        {
            if (param.IsDenormalized)
            {
                CodeSnippetTypeMember snippet = new CodeSnippetTypeMember();
                if (!string.IsNullOrEmpty(param.Comment))
                    snippet.Comments.Add(new CodeCommentStatement(param.Comment, true));
                snippet.Text = "        " + ScopeToString(param.Scope) + " " + param.Type + " " + param.Name + " { get; set; }";
                parent.Members.Add(snippet);
            }
            else
            {
                CodeMemberProperty prop = new CodeMemberProperty();

                prop.Name = param.Name;
                prop.Attributes = MemberAttributes.Public | MemberAttributes.Final;
                prop.HasGet = true;
                //   prop.HasSet = true;
                prop.Type = new CodeTypeReference(param.Type);

                parent.Members.Add(prop);
            }
        }

        private string ScopeToString(Yis.Designer.Software.Model.Scope scope)
        {
            string re = string.Empty;

            switch (scope)
            {
                case Yis.Designer.Software.Model.Scope.Public:
                    re = "public";
                    break;

                case Yis.Designer.Software.Model.Scope.Private:
                    re = "private";
                    break;

                case Yis.Designer.Software.Model.Scope.Protected:
                    re = "protected";
                    break;

                default:
                    re = "public";
                    break;
            }

            return re;
        }

        #endregion Methods
    }
}