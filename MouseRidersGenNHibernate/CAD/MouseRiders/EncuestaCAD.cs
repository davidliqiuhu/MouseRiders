
using System;
using System.Text;
using MRModel.CEN;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Exceptions;
using MRModel.EN;
using MRModel.Exceptions;


/*
 * Clase Encuesta:
 *
 */

namespace MRModel.CAD
{
public partial class EncuestaCAD : BasicCAD, IEncuestaCAD
{
public EncuestaCAD() : base ()
{
}

public EncuestaCAD(ISession sessionAux) : base (sessionAux)
{
}



public EncuestaEN ReadOIDDefault (int id
                                  )
{
        EncuestaEN encuestaEN = null;

        try
        {
                SessionInitializeTransaction ();
                encuestaEN = (EncuestaEN)session.Get (typeof(EncuestaEN), id);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return encuestaEN;
}

public System.Collections.Generic.IList<EncuestaEN> ReadAllDefault (int first, int size)
{
        System.Collections.Generic.IList<EncuestaEN> result = null;
        try
        {
                using (ITransaction tx = session.BeginTransaction ())
                {
                        if (size > 0)
                                result = session.CreateCriteria (typeof(EncuestaEN)).
                                         SetFirstResult (first).SetMaxResults (size).List<EncuestaEN>();
                        else
                                result = session.CreateCriteria (typeof(EncuestaEN)).List<EncuestaEN>();
                }
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }

        return result;
}

// Modify default (Update all attributes of the class)

public void ModifyDefault (EncuestaEN encuesta)
{
        try
        {
                SessionInitializeTransaction ();
                EncuestaEN encuestaEN = (EncuestaEN)session.Load (typeof(EncuestaEN), encuesta.Id);

                encuestaEN.Titulo = encuesta.Titulo;



                encuestaEN.Descripcion = encuesta.Descripcion;

                session.Update (encuestaEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }
}


public int CrearEncuesta (EncuestaEN encuesta)
{
        try
        {
                SessionInitializeTransaction ();

                session.Save (encuesta);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return encuesta.Id;
}

public void ModificarEncuesta (EncuestaEN encuesta)
{
        try
        {
                SessionInitializeTransaction ();
                EncuestaEN encuestaEN = (EncuestaEN)session.Load (typeof(EncuestaEN), encuesta.Id);

                encuestaEN.Titulo = encuesta.Titulo;


                encuestaEN.Descripcion = encuesta.Descripcion;

                session.Update (encuestaEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }
}
public void BorrarEncuesta (int id
                            )
{
        try
        {
                SessionInitializeTransaction ();
                EncuestaEN encuestaEN = (EncuestaEN)session.Load (typeof(EncuestaEN), id);
                session.Delete (encuestaEN);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }
}

//Sin e: ReadOID
//Con e: EncuestaEN
public EncuestaEN ReadOID (int id
                           )
{
        EncuestaEN encuestaEN = null;

        try
        {
                SessionInitializeTransaction ();
                encuestaEN = (EncuestaEN)session.Get (typeof(EncuestaEN), id);
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return encuestaEN;
}

public System.Collections.Generic.IList<EncuestaEN> ReadAll (int first, int size)
{
        System.Collections.Generic.IList<EncuestaEN> result = null;
        try
        {
                SessionInitializeTransaction ();
                if (size > 0)
                        result = session.CreateCriteria (typeof(EncuestaEN)).
                                 SetFirstResult (first).SetMaxResults (size).List<EncuestaEN>();
                else
                        result = session.CreateCriteria (typeof(EncuestaEN)).List<EncuestaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}

public System.Collections.Generic.IList<MRModel.EN.EncuestaEN> ReadFilter (string p_titulo)
{
        System.Collections.Generic.IList<MRModel.EN.EncuestaEN> result;
        try
        {
                SessionInitializeTransaction ();
                //String sql = @"FROM EncuestaEN self where FROM EncuestaEN where :p_titulo like titulo or :p_titulo like descripcion";
                //IQuery query = session.CreateQuery(sql);
                IQuery query = (IQuery)session.GetNamedQuery ("EncuestaENreadFilterHQL");
                query.SetParameter ("p_titulo", p_titulo);

                result = query.List<MRModel.EN.EncuestaEN>();
                SessionCommit ();
        }

        catch (Exception ex) {
                SessionRollBack ();
                if (ex is MRModel.Exceptions.ModelException)
                        throw ex;
                throw new MRModel.Exceptions.DataLayerException ("Error in EncuestaCAD.", ex);
        }


        finally
        {
                SessionClose ();
        }

        return result;
}
}
}
