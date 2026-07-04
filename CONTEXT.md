# NetworkLab - CONTEXT.md

## Descripción del proyecto

**NetworkLab** es un proyecto educativo desarrollado en **C# (.NET 10)** cuyo objetivo es aprender redes IPv4 implementando todo manualmente, sin depender de las clases del framework (`System.Net.IPAddress`, etc.).

El proyecto también sirve para recuperar práctica programando después de haber dependido demasiado de herramientas de IA ("vibecoding"), por lo que el objetivo principal es **comprender y diseñar**, no simplemente terminar el código.

En el futuro se contempla portar el proyecto a **Rust**, por lo que se intenta mantener un diseño limpio y desacoplado.

---

# Estructura de la solución

```
NetworkLab.slnx

NetworkLab.Core
└── Models
    ├── IPv4Address.cs
    ├── IPv4Mask.cs
    └── IPv4Network.cs

NetworkLab.Cli
```

## Responsabilidades

### NetworkLab.Core

Contiene toda la lógica del dominio.

No debe depender de la interfaz de usuario.

Aquí viven todas las clases relacionadas con IPv4.

### NetworkLab.Cli

Proyecto de consola utilizado para realizar pruebas, experimentar con la API y validar el funcionamiento de las clases del Core.

---

# Filosofía del proyecto

Durante el desarrollo se acordaron las siguientes reglas:

* No utilizar `System.Net.IPAddress`.
* Implementar todos los algoritmos manualmente.
* Priorizar claridad antes que microoptimizaciones.
* Modelar conceptos reales del dominio de redes.
* Evitar almacenar información duplicada.
* Cada clase debe tener una única responsabilidad.
* No implementar funcionalidades "por si acaso".
* Añadir nuevas características únicamente cuando aparezca una necesidad real.

---

# Estilo de aprendizaje

ChatGPT actúa como Tech Lead.

El objetivo no es entregar soluciones completas inmediatamente.

La dinámica consiste en:

* Explicar primero el razonamiento.
* Justificar las decisiones de diseño.
* Enseñar el funcionamiento interno de cada algoritmo.
* Avanzar mediante pequeños sprints.
* Incentivar que el alumno implemente el código mientras comprende cada paso.

La prioridad siempre es aprender a diseñar software y entender el porqué de cada decisión.

---

# Estado actual del proyecto

## IPv4Address

Representa una dirección IPv4.

### Diseño

La dirección se almacena internamente como un único entero de 32 bits.

```csharp
private readonly uint _value;
```

No existen cuatro atributos para los octetos.

Los octetos se calculan dinámicamente mediante propiedades.

```csharp
Octet1
Octet2
Octet3
Octet4
```

Esto evita almacenar información redundante.

### Funcionalidades implementadas

* Constructor desde cuatro octetos.
* Conversión interna mediante `Pack()`.
* `Parse(string)` implementado completamente de forma manual.
* Validación de:

  * cantidad de octetos
  * rango 0-255
  * caracteres inválidos
  * puntos consecutivos
  * punto final
  * ceros a la izquierda
* `ToString()`
* `FromUInt32(uint)`
* Propiedad `Value`.

---

## Parser IPv4

El parser fue implementado carácter por carácter.

No utiliza:

* Split()
* Regex
* IPAddress
* Métodos de parsing del framework

Durante su implementación se discutieron varios enfoques para validar los ceros a la izquierda.

Finalmente se adoptó una solución basada en variables de estado que privilegia la legibilidad frente a soluciones demasiado compactas.

---

## IPv4Mask

Representa una máscara IPv4.

### Diseño

Internamente también almacena únicamente un entero de 32 bits.

```csharp
private readonly uint _value;
```

La máscara se crea exclusivamente mediante:

```csharp
IPv4Mask.FromPrefix(byte prefix)
```

No existe constructor público.

### Funcionalidades implementadas

* Validación del prefijo (0-32).
* Conversión de prefijo CIDR a máscara mediante desplazamientos de bits.
* Propiedad `Value`.
* `ToString()` reutilizando `IPv4Address.FromUInt32()`.

No se implementó todavía:

* PrefixLength.
* Parse().
* Equals().
* GetHashCode().
* Operadores.

Estas funcionalidades se añadirán únicamente cuando exista una necesidad real.

---

## IPv4Network

Actualmente se encuentra en desarrollo.

Estado actual:

```csharp
private readonly IPv4Address _networkAddress;
private readonly byte _prefixLength;
```

Actualmente solo existe el esqueleto de la clase.

Todavía no implementa el cálculo de la red.

---

# Decisiones importantes de diseño

## Una única fuente de verdad

IPv4Address almacena únicamente:

```csharp
uint
```

IPv4Mask almacena únicamente:

```csharp
uint
```

Toda la información restante se deriva a partir de ese valor.

---

## Conceptos del dominio

Se decidió modelar explícitamente los conceptos del mundo de redes.

Por ello existen clases distintas para:

* IPv4Address
* IPv4Mask
* IPv4Network

Aunque internamente todas utilicen un `uint`, representan conceptos diferentes.

---

## Evitar duplicidad

Siempre que una información pueda calcularse a partir de otra, se prefiere calcularla antes que almacenarla.

---

# Objetivo final

Construir una pequeña librería educativa capaz de realizar cálculos completos de IPv4 y subnetting implementando todos los algoritmos desde cero y utilizando un diseño limpio, extensible y orientado al dominio.
