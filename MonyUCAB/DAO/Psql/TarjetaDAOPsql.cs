﻿using MonyUCAB.DTO;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonyUCAB.DAO.Psql
{
    public class TarjetaDAOPsql : DAOPsql, ITarjetaDAO
    {
        public TarjetaDTO buscarTarjeta(int idTarjeta)
        {
            try
            {
                comando.CommandText = string.Format("SELECT" + 
                "buscarTarjeta({0})", idTarjeta);
                conexion.Open();
                filas = comando.ExecuteReader();
                TarjetaDTO tarjetaDTO = null;
                if (filas.Read())
                {
                    tarjetaDTO = new TarjetaDTO
                    {
                        Idtarjeta = filas.GetInt32(0),
                        Idusuario = filas.GetInt32(1),
                        Idtipotarjeta = filas.GetInt32(2),
                        Idbanco = filas.GetInt32(3),
                        Numero = filas.GetInt32(4),
                        Fecha_vencimiento = filas.GetDateTime(5),
                        Cvc = filas.GetInt32(6),
                        Estatus = filas.GetInt32(7),
                    };
                }
                filas.Close();
                return tarjetaDTO;
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

        public List<TarjetaDTO> buscarTarjetas(int idUsuario)
        {
            try
            {
                comando.CommandText = string.Format("SELECT" + 
                "buscarTarjetas({0})", idUsuario);
                conexion.Open();
                filas = comando.ExecuteReader();
                List<TarjetaDTO> tarjetaDTOs = new List<TarjetaDTO>();
                while (filas.Read())
                {
                    tarjetaDTOs.Add(new TarjetaDTO
                    {
                        Idtarjeta = filas.GetInt32(0),
                        Idusuario = filas.GetInt32(1),
                        Idtipotarjeta = filas.GetInt32(2),
                        Idbanco = filas.GetInt32(3),
                        Numero = filas.GetInt32(4),
                        Fecha_vencimiento = filas.GetDateTime(5),
                        Cvc = filas.GetInt32(6),
                        Estatus = filas.GetInt32(7),
                    });
                }
                filas.Close();
                return tarjetaDTOs;
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