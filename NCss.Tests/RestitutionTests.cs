﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NCss.Tests
{
    [TestFixture]
    public class RestitutionTests
    {
        [Test]
        public void UnparsableProperties()
        {
            var p = new CssParser().ParseSheet(".class{ldsk}");
            Assert.AreEqual(".class{ldsk}", p.ToString(CssRestitution.OriginalWhenErrorOrInvalid));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.OnlyWhatYouUnderstood));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.RemoveErrors));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.RemoveInvalid));
            p = new CssParser().ParseSheet(".class{ldsk%xk}");
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.OnlyWhatYouUnderstood));
            Assert.AreEqual(".class{ldsk%xk}", p.ToString(CssRestitution.OriginalWhenErrorOrInvalid));
        }

        [Test]
        public void InvalidPropertyValue()
        {
            var p = new CssParser().ParseSheet(".class{prop:#ffff;}");
            Assert.AreEqual(".class{prop:#ffff;}", p.ToString(CssRestitution.OriginalWhenErrorOrInvalid));
            Assert.AreEqual(".class{prop:#ffff;}", p.ToString(CssRestitution.OnlyWhatYouUnderstood));
            Assert.AreEqual(".class{prop:#ffff;}", p.ToString(CssRestitution.RemoveErrors));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.RemoveInvalid));

            p = new CssParser().ParseSheet(".class{prop:#ffff;msldkqj;}");
            Assert.AreEqual(".class{prop:#ffff;msldkqj;}", p.ToString(CssRestitution.OriginalWhenErrorOrInvalid));
            Assert.AreEqual(".class{prop:#ffff;}", p.ToString(CssRestitution.OnlyWhatYouUnderstood));
            Assert.AreEqual(".class{prop:#ffff;}", p.ToString(CssRestitution.RemoveErrors));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.RemoveInvalid));

            p = new CssParser().ParseSheet(".class{prop:#ffff;msldkqj;prop:;}");
            Assert.AreEqual(".class{prop:#ffff;msldkqj;prop:;}", p.ToString(CssRestitution.OriginalWhenErrorOrInvalid));
            Assert.AreEqual(".class{prop:#ffff;prop:;}", p.ToString(CssRestitution.OnlyWhatYouUnderstood));
            Assert.AreEqual(".class{prop:#ffff;prop:;}", p.ToString(CssRestitution.RemoveErrors));
            Assert.AreEqual(".class{}", p.ToString(CssRestitution.RemoveInvalid));
        }
    }
}
