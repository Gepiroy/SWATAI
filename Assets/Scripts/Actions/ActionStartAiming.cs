using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ActionShootFromCover : Action
{
    public ActionShootFromCover(): base(0.3f)
    {
    }

    public override void Start()
    {
        _unit.SetAiming(true);
        _unit.SetCrouching(false);
    }

    protected override void OnFixedUpdate()
    {
    }
}
