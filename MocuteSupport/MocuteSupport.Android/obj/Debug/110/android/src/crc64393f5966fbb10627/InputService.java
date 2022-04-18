package crc64393f5966fbb10627;


public class InputService
	extends android.inputmethodservice.InputMethodService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:()V:GetOnCreateHandler\n" +
			"n_onGenericMotionEvent:(Landroid/view/MotionEvent;)Z:GetOnGenericMotionEvent_Landroid_view_MotionEvent_Handler\n" +
			"n_onKeyDown:(ILandroid/view/KeyEvent;)Z:GetOnKeyDown_ILandroid_view_KeyEvent_Handler\n" +
			"n_onKeyUp:(ILandroid/view/KeyEvent;)Z:GetOnKeyUp_ILandroid_view_KeyEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("MocuteSupport.Droid.InputService, MocuteSupport.Android", InputService.class, __md_methods);
	}


	public InputService ()
	{
		super ();
		if (getClass () == InputService.class)
			mono.android.TypeManager.Activate ("MocuteSupport.Droid.InputService, MocuteSupport.Android", "", this, new java.lang.Object[] {  });
	}


	public void onCreate ()
	{
		n_onCreate ();
	}

	private native void n_onCreate ();


	public boolean onGenericMotionEvent (android.view.MotionEvent p0)
	{
		return n_onGenericMotionEvent (p0);
	}

	private native boolean n_onGenericMotionEvent (android.view.MotionEvent p0);


	public boolean onKeyDown (int p0, android.view.KeyEvent p1)
	{
		return n_onKeyDown (p0, p1);
	}

	private native boolean n_onKeyDown (int p0, android.view.KeyEvent p1);


	public boolean onKeyUp (int p0, android.view.KeyEvent p1)
	{
		return n_onKeyUp (p0, p1);
	}

	private native boolean n_onKeyUp (int p0, android.view.KeyEvent p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
