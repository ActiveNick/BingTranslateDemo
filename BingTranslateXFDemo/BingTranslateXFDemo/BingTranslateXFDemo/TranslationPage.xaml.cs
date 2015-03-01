using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingAPILibrary;

namespace BingTranslateXFDemo
{
	public partial class TranslationPage
	{
		public TranslationPage ()
		{
			InitializeComponent ();
		}

        public async void OnTranslateClicked(object sender, EventArgs args)
        {
            Translator tr = new Translator();

            lblResult.Text = await tr.TranslateString(txtSource.Text);
        }
	}
}
