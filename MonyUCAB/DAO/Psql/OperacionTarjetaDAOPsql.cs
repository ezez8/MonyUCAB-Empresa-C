﻿using MonyUCAB.DTO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonyUCAB.DAO.Psql
{
    public class OperacionTarjetaDAOPsql : DAOPsql, IOperacionTarjetaDAO
    {
        public OperacionTarjetaDTO buscarOperacionTarjeta(int idOperacionTarjeta)
        {
            try
            {
                comando.CommandText = string.Format("SELECT" + 
                "buscarOperacionTarjeta({0})", idOperacionTarjeta);
                conexion.Open();
                filas = comando.ExecuteReader();
                OperacionTarjetaDTO operacionTarjetaDTO = null;
                if (filas.Read())
                {
                    operacionTarjetaDTO = new OperacionTarjetaDTO
                    {
                        Idoperaciontarjeta = filas.GetInt32(0),
                        IdUsuarioReceptor = filas.GetInt32(1),
                        Idtarjeta = filas.GetInt32(2),
                        Fecha = filas.GetDateTime(3),
                        Hora = filas.GetTimeSpan(4),
                        Monto = filas.GetFloat(5),
                        Referencia = filas.GetInt32(6),
                    };
                }
                filas.Close();
                return operacionTarjetaDTO;
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
        }

        public List<OperacionTarjetaDTO> buscarOperacionesTarjetas(int idUsuario)
        {
            try
            {
                comando.CommandText = string.Format("SELECT" + 
                "buscarOperacionesTarjetas({0})", idUsuario);
                conexion.Open();
                filas = comando.ExecuteReader();
                List<OperacionTarjetaDTO> operacionTarjetaDTOs = new List<OperacionTarjetaDTO>();
                while (filas.Read())
                {
                    operacionTarjetaDTOs.Add(new OperacionTarjetaDTO
                    {
                        Idoperaciontarjeta = filas.GetInt32(0),
                        IdUsuarioReceptor = filas.GetInt32(1),
                        Idtarjeta = filas.GetInt32(2),
                        Fecha = filas.GetDateTime(3),
                        Hora = filas.GetTimeSpan(4),
                        Monto = filas.GetFloat(5),
                        Referencia = filas.GetInt32(6),
                    });
                }
                filas.Close();
                return operacionTarjetaDTOs;
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
        }

        public void realizar(int idTarjeta, string usuarioReceptor, float monto, int referencia)
        {
            try
            {
                comando.CommandText = string.Format("SELECT" + 
                "OperacionTarjetaDAOPsqlrealizar({0}, '{1}', {2}, {3})",
                idTarjeta, usuarioReceptor, monto, referencia);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (NpgsqlException e)
            {
                throw e;
            }
            finally
            {
                conexion.Close();
            }
        }
    }
}