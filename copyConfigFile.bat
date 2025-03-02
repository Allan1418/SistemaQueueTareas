@echo off

if not exist "connections" (
  mkdir "connections"
)

copy "database.config" "connections\database.config"