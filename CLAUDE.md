# QuantumAE.API - REST API projekt

> Általános szabályok: lásd `../QuantumAE/CLAUDE.md`

## Elnevezési kivétel

API modellekhez (Request/Response) **nem kell T prefix**:
```csharp
record LoginRequest(...);         // ✓ Request/Response: nincs prefix
record LoginResponse(...);        // ✓
class TUserService { }            // ✓ Egyéb osztályok: T prefix marad
```

## Projekt struktúra

| Könyvtár | Tartalom |
|----------|----------|
| `src/QuantumAE.API/` | REST API szerver (ASP.NET Core) |
| `src/QuantumAE.API.Client/` | API kliens könyvtár |
| `src/QuantumAE.CLI/` | Parancssori eszközök |
| `tests/QuantumAE.API.Test/` | API tesztek |

## API szabványok

- **ProblemDetails** használata hibaválaszokhoz
- Bemenetek validálása (DataAnnotations)
- Hitelesített végpontok alapértelmezetten

## Kapcsolódó projekt

Ez a projekt a `../QuantumAE/` solution része is. Lásd: `../QuantumAE/CLAUDE.md`
