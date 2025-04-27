# OCR Search API

API para upload de documentos (PDFs e imagens), extração de texto via OCR e indexação no Elasticsearch, permitindo busca
full-text.

---

## Visão Geral

Este projeto visa criar um sistema de **busca inteligente em documentos** por meio de:

- Upload de arquivos (PDF, PNG, JPG/JPEG)
- Extração de texto com OCR (Tesseract)
- Indexação de conteúdo e metadados no Elasticsearch
- API de busca com filtros e relevância

---

## Tecnologias Utilizadas

| Camada          | Tecnologia              |
|-----------------|-------------------------|
| Backend         | ASP.NET Core Web API    |
| OCR             | Tesseract OCR           |
| Busca           | Elasticsearch           |
| Containerização | Docker + Docker Compose |
| Outros          | Scalar                  |

---

## Funcionalidades

- [x] Upload de arquivos PDF e imagens
- [x] Extração de texto com OCR
- [x] Indexação no Elasticsearch
- [x] API REST para busca textual
- [x] Highlight de termos encontrados
- [ ] Filtros por tipo de documento, data, etc.

---

## Estrutura do Projeto

```bash
/OCRSearch.Application       # Lógica de negócio
/OCRSearch.Domain            # Entidades e contratos
/OCRSearch.Infrastructure    # OCR, Elasticsearch, repositórios
/OCRSearch.API               # Controllers e configuração
compose.yml # Elasticsearch + Kibana
```

---

## Setup do Projeto

### Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download) (se quiser rodar localmente)
- [Docker](https://www.docker.com/)

### Rodando API, Elasticsearch e Kibana com Docker

```bash
docker-compose up --build -d
```

Acesse o Kibana: [http://localhost:5601](http://localhost:5601)

### Rodando API localmente

```bash
docker-compose up --build -d
docker stop ocr-search-api
cd OCRSearch.Api && dotnet run
```

Acesse o Scalar: [http://localhost:5174/scalar/v1](http://localhost:5174/scalar/v1)

---

## Endpoints

### Upload de Documento

```http
POST /Upload-File
Content-Type: multipart/form-data
```

**Key:**

- file

**Value**

- Arquivo (PDF e imagem)

---

### Buscar Documento

```http
GET /Search-File?content=texto&size=
```

**Query Params:**

- `content`: Termo de busca
- `size`: Quantos documentos retornar

**Retorno:**

```json
[
  {
    "id": "abc123",
    "name": "test.png",
    "extension": ".png",
    "url": "https://some-url",
    "extractedText": "bla bla bla bla bla",
    "uploadedAt": "2025-04-27 15:42:30"
  }
]
```

---

## Licença

Projeto open-source sob a licença MIT. Sinta-se à vontade para usar, modificar e contribuir!
