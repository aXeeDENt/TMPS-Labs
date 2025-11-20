using System;

namespace models
{
    public interface IPlayer
    {
        Role role { get; set; }
        Primary_Weapon primary_weapon { get; set; }
        Secondary_Weapon secondary_weapon { get; set; }
        Gadget gadget { get; set; }

        void Show();
    }
}
