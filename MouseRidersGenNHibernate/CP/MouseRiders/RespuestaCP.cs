
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
public partial class RespuestaCP : BasicCP
{
public RespuestaCP() : base ()
{
}

public RespuestaCP(ISession sessionAux)
        : base (sessionAux)
{
}
}
}
