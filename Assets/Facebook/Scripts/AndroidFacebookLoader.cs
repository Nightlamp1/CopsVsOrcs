<<<<<<< HEAD
using UnityEngine;
using System.Collections;

namespace Facebook
{
    public class AndroidFacebookLoader : FB.CompiledFacebookLoader
    {

        protected override IFacebook fb
        {
            get
            {
                return FBComponentFactory.GetComponent<AndroidFacebook>();
            }
        }
    }
=======
using UnityEngine;
using System.Collections;

namespace Facebook
{
    public class AndroidFacebookLoader : FB.CompiledFacebookLoader
    {

        protected override IFacebook fb
        {
            get
            {
                return FBComponentFactory.GetComponent<AndroidFacebook>();
            }
        }
    }
>>>>>>> a28d458a04d2696f92ff85ac198aff05512403b3
}