package crc64393f5966fbb10627;


public class InputAccessibilityService
	extends android.accessibilityservice.AccessibilityService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onServiceConnected:()V:GetOnServiceConnectedHandler\n" +
			"n_onAccessibilityEvent:(Landroid/view/accessibility/AccessibilityEvent;)V:GetOnAccessibilityEvent_Landroid_view_accessibility_AccessibilityEvent_Handler\n" +
			"n_onInterrupt:()V:GetOnInterruptHandler\n" +
			"";
		mono.android.Runtime.register ("MocuteSupport.Droid.InputAccessibilityService, MocuteSupport.Android", InputAccessibilityService.class, __md_methods);
	}


	public InputAccessibilityService ()
	{
		super ();
		if (getClass () == InputAccessibilityService.class)
			mono.android.TypeManager.Activate ("MocuteSupport.Droid.InputAccessibilityService, MocuteSupport.Android", "", this, new java.lang.Object[] {  });
	}


	public void onServiceConnected ()
	{
		n_onServiceConnected ();
	}

	private native void n_onServiceConnected ();


	public void onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0)
	{
		n_onAccessibilityEvent (p0);
	}

	private native void n_onAccessibilityEvent (android.view.accessibility.AccessibilityEvent p0);


	public void onInterrupt ()
	{
		n_onInterrupt ();
	}

	private native void n_onInterrupt ();

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
