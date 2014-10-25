<<<<<<< HEAD
using UnityEngine;
using System.Collections;

namespace Facebook
{
    public class EditorFacebookLoader : FB.CompiledFacebookLoader
    {

        protected override IFacebook fb
        {
            get
            {
                return FBComponentFactory.GetComponent<EditorFacebook>();
            }
        }
    }
=======
using UnityEngine;
using System.Collections;

namespace Facebook
{
    public class EditorFacebookLoader : FB.CompiledFacebookLoader
    {

        protected override IFacebook fb
        {
            get
            {
                return FBComponentFactory.GetComponent<EditorFacebook>();
            }
        }
    }
>>>>>>> a28d458a04d2696f92ff85ac198aff05512403b3
}