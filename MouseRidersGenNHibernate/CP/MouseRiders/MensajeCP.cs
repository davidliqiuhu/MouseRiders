
using System;
using System.Text;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using MouseRidersGenNHibernate.Exceptions;
using MouseRidersGenNHibernate.EN.MouseRiders;
using MouseRidersGenNHibernate.CAD.MouseRiders;
using MouseRidersGenNHibernate.CEN.MouseRiders;


namespace MouseRidersGenNHibernate.CP.MouseRiders
{
public partial class MensajeCP : BasicCP
{
public MensajeCP() : base ()
{
}

public MensajeCP(ISession sessionAux)
        : base (sessionAux)
{
}
}
}
