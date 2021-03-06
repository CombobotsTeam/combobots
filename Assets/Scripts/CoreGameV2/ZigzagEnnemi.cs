﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.CoreGameV2
{
    class ZigzagEnnemi : BasicEnnemy
    {
        float angle = 0;
        public float verticalSpeed = 1f;

        protected override void Move()
        {
            if (IsMoving)
            {
                angle += verticalSpeed * slow;
                if (angle > 360)
                    angle -= 360;
                Position.x += Mathf.Cos(angle * Mathf.Deg2Rad) * 0.2f * (reverse ? -1 : 1);
                base.Move();
            }
        }
    }
}
