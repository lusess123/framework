using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Ataw.Framework.Core
{
    
    public static class XmlSchemaObjectCollectionExt
  {

      public static  List<XmlSchemaObject> ToList(this XmlSchemaObjectCollection coll)
        {
            List<XmlSchemaObject> list = new List<XmlSchemaObject>();
            foreach (var bean in coll)
            {
                list.Add(bean);
            }
            return list;
        }
    }

    public enum XsdItmType
    {
        None = 0 ,
        ComplexDefObj,
        SimpleObj,
        ComplexObj,
        AttriObj,
        EnumObj,
        EnumObjDef ,
        XsdFile
    }


    public class XsdItem
    {
      
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsLeaf { get; set; }
        public List<XsdItem> Children { get; set; }
        private XmlSchemaAnnotated Ann { get; set; }
        // public XmlSchemaObject XmlObject { get; set; }

        private XmlSchemaType XmlTypeObj { get; set; }
       

        private XmlSchemaComplexType ComplexObj { get; set; }

        private XmlSchemaSimpleType SimpleObj { get; set; }

        public string XsdItmTypeStr { get; set; }

        public string ContentType { get; set; }

        public string TypeName { get; set; }

        public string DataType { get; set; }

        public string Use { get; set; }

        public void DisplayObjects(string indent)
        {
            Console.WriteLine(indent + this.Name + ":" + this.Text);
            string _str = indent + "\t";
            Children.ForEach(a =>
            {

                Console.WriteLine(_str + a.Name + ":" + a.Text);

            });
        }

        public XsdItem()
        {
           // Children = new List<XsdItem>();
        }

       

        public XsdItem(XmlSchemaObject a):this()
        {
            TypeName = a.GetType().ToString();
           // Children = new List<XsdItem>();
            IsLeaf = true;
            Ann = a as XmlSchemaAnnotated;
            //XmlObject = a as XmlSchemaObject;
            XmlTypeObj = a as XmlSchemaType;

            ComplexObj = a as XmlSchemaComplexType;
            SimpleObj = a as XmlSchemaSimpleType;

            if (XmlTypeObj != null)
            {
                Name = XmlTypeObj.Name;
                //if (XmlTypeObj.TypeCode != null)
                ContentType = XmlTypeObj.TypeCode.ToString();
            }
            else
            {
                if (a is XmlSchemaAttribute) {
                    XsdItmTypeStr = XsdItmType.AttriObj.ToString();
                    XmlSchemaAttribute att = a as XmlSchemaAttribute;
                    Name = att.Name;
                    //this.IsAttr = true;
                    //this.IsSimple = false;
                   // this.ContentType = att.
                    this.DataType = att.SchemaTypeName.Name;
                    this.Use = att.Use.ToString();
                }
            
            }

            if (Ann != null)
            {
                if (Ann.Annotation != null)
                    Text = DisplayDoc(Ann.Annotation.Items);
            }
            //if(a is){
            //}

            if (a is XmlSchemaElement)
            {
                var o = ((XmlSchemaElement)a);
                Name = o.Name;
                this.DataType = o.SchemaTypeName.Name;

                if (o.ElementSchemaType is XmlSchemaSimpleType)
                {
                    XsdItmTypeStr = XsdItmType.SimpleObj.ToString();
                    this.IsLeaf = true;
                }
                else
                {
                    XsdItmTypeStr = XsdItmType.ComplexObj.ToString();
                    this.IsLeaf = false;
                   
                }
                //this.Use = 

            }
            if (a is XmlSchemaEnumerationFacet)
            {
                XsdItmTypeStr = XsdItmType.EnumObj.ToString();
                XmlSchemaEnumerationFacet f = a as XmlSchemaEnumerationFacet;
                Name = f.Value;
                if (f.Annotation != null)
                {
                    Text = DisplayDoc((f).Annotation.Items);
                }
            }

            if (ComplexObj != null)
            {
                XsdItmTypeStr = XsdItmType.ComplexDefObj.ToString();
                //this.IsSimple = false;
                ContentType = ComplexObj.ContentType.ToString();
                IsLeaf = false;
                
                foreach (var attr in ComplexObj.Attributes)
                {
                    var _item = new XsdItem(attr);
                    if (this.Children == null)
                    {
                        this.Children  = new List<XsdItem>();
                    }
                    this.Children.Add(_item);
                }

                var _p = ComplexObj.ContentTypeParticle;
                XmlSchemaChoice _c = _p as XmlSchemaChoice;
                if (_c != null)
                {
                    // _c.Items
                    foreach (var gg in _c.Items)
                    {
                        if (this.Children == null)
                        {
                            this.Children = new List<XsdItem>();
                        }
                        var _item = new XsdItem(gg);
                        this.Children.Add(_item);
                    }
                }
            }
            if (SimpleObj != null)
            {
                //this.IsSimple = true;
                if (SimpleObj.BaseXmlSchemaType != null)
                {
                    this.ContentType = SimpleObj.BaseXmlSchemaType.QualifiedName.Name;
                }
                var _y = SimpleObj.Content;
                XmlSchemaSimpleTypeRestriction ff = _y as XmlSchemaSimpleTypeRestriction;
                if (ff != null)
                {
                    this.XsdItmTypeStr = XsdItmType.EnumObjDef.ToString();
                    foreach (var gg in ff.Facets)
                    {
                        if (this.Children == null)
                        {
                            this.Children = new List<XsdItem>();
                        }
                        var _item = new XsdItem(gg);
                        this.Children.Add(_item);
                    }

                }

                // _y.r
            }

        }

        private static string DisplayDoc(XmlSchemaObjectCollection _list)
        {
            string _str = "";
            foreach (var i in _list)
            {
                if (i is XmlSchemaDocumentation)
                {
                    XmlSchemaDocumentation v = i as XmlSchemaDocumentation;
                    _str += (string.Join(",", v.Markup.Select(a => a.Value)));
                }
            }
            return _str;
        }

    }
}
