# GraphQL

-   Testing local: https://localhost:32791/graphql/
-   Testing on Docker: http://localhost:32790/graphql/

![alt text](../../img/GraphQL.jpg)

## Query Examples

### Sorting

<details>
    <summary>Collapse</summary>

-   Sorting by Name

    ```gql
    {
        medics(order: [{ name: ASC }]) {
            name
            medicalSpecialtyId
            appointments {
                schedule
            }
        }
    }
    ```

</details>

### Filtering

<details>
    <summary>Collapse</summary>

-   Filtering on name contains:

    ```gql
    {
        patients(where: { name: { contains: "ph" } }) {
            name
            address
            phone
            email
            appointments {
                id
                schedule
                medicId
            }
        }
    }
    ```

-   Filtering on name equals:
    ```gql
    {
        patients(where: { name: { eq: "Alexander Hughes" } }) {
            name
            address
            phone
            email
            appointments {
                id
                schedule
                medicId
            }
        }
    }
    ```

</details>

### Pagination

<details>
    <summary>Collapse</summary>

Managed to get it working by adding the `[UsePaging]` decorator only on the query - **Query.cs** - but on the models:

```csharp
...
[UsePaging]
[UseProjection]
[UseFiltering]
[UseSorting]
public IQueryable<Patient> GetPatients([Service] PostgreContext context) => context.Patients;
...
```

-   Getting the first 2 records:

    ```gql
      {
          // Ordered by name and getting 2 records
          // (I just have 5 records by the time I was running the code 😜)
          patients( order: [ { name: ASC }] first: 2) {
              edges {
                  node {
                      id
                      name
                  }
                  cursor                  // shows the cursor position of the current record
                  }
                  pageInfo {
                      hasNextPage         // boolean, indicates if a next page exists
                      hasPreviousPage     // boolean, indicates if a previous page exists
                      startCursor         // Indicates the value of the first cursor on the current query
                      endCursor           // Indicates the value of the last cursor on the current query
              }
          }
      }
    ```

    Response:

    ```json
    {
        "data": {
            "patients": {
                "edges": [
                    {
                        "node": {
                            "id": "b681485e-a062-4e71-9272-96e610e5bd36",
                            "name": "Alexander Hughes"
                        },
                        "cursor": "MA=="
                    },
                    {
                        "node": {
                            "id": "e916e089-3681-464f-9fe6-1cc522280800",
                            "name": "Harper Collins"
                        },
                        "cursor": "MQ=="
                    }
                ],
                "pageInfo": {
                    "hasNextPage": true,
                    "hasPreviousPage": false,
                    "startCursor": "MA==",
                    "endCursor": "MQ=="
                }
            }
        }
    }
    ```

-   Getting the next 2 records: Note the `after: "MQ=="` setted with the value of `endCursor`

    ```gql
    {
        patients(order: [{ name: ASC }], first: 2, after: "MQ==") {
            edges {
                node {
                    id
                    name
                }
                cursor
            }
            pageInfo {
                hasNextPage
                hasPreviousPage
                startCursor
                endCursor
            }
        }
    }
    ```

    Response:

    ```json
    {
        "data": {
            "patients": {
                "edges": [
                    {
                        "node": {
                            "id": "e5dcab66-2937-43ec-8976-0eac94cbd104",
                            "name": "Mia Rivera"
                        },
                        "cursor": "Mg=="
                    },
                    {
                        "node": {
                            "id": "1e58ace1-df38-4f32-b9ae-8f98f7375265",
                            "name": "Noah Reed"
                        },
                        "cursor": "Mw=="
                    }
                ],
                "pageInfo": {
                    "hasNextPage": true,
                    "hasPreviousPage": true,
                    "startCursor": "Mg==",
                    "endCursor": "Mw=="
                }
            }
        }
    }
    ```

-   Getting the previous 2 records: Note the `before: "Mg=="` setted with the value of `startCursor`

    ```gql
    {
        patients(order: [{ name: ASC }], first: 2, before: "Mg==") {
            edges {
                node {
                    id
                    name
                }
                cursor
            }
            pageInfo {
                hasNextPage
                hasPreviousPage
                startCursor
                endCursor
            }
        }
    }
    ```

    Response:

    ```json
    {
        "data": {
            "patients": {
                "edges": [
                    {
                        "node": {
                            "id": "b681485e-a062-4e71-9272-96e610e5bd36",
                            "name": "Alexander Hughes"
                        },
                        "cursor": "MA=="
                    },
                    {
                        "node": {
                            "id": "e916e089-3681-464f-9fe6-1cc522280800",
                            "name": "Harper Collins"
                        },
                        "cursor": "MQ=="
                    }
                ],
                "pageInfo": {
                    "hasNextPage": true,
                    "hasPreviousPage": false,
                    "startCursor": "MA==",
                    "endCursor": "MQ=="
                }
            }
        }
    }
    ```

</details>

## Caveats

<details>
    <summary>Collapse</summary>

-   [UseSorting] decorator on the models is causing execution errors.

```csharp
public class Medic
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Address { get; set; }
    [Phone]
    public string Phone { get; set; }
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    //[UseSorting] // Execution errors
    [ForeignKey(nameof(MedicalSpecialty))]
    public List<Guid> MedicalSpecialtyId { get; set; }

    [JsonIgnore]
    //[UseSorting] // Execution errors
    public List<Appointment> Appointments { get; } = new();

}
```

-   `.RegisterDbContextFactory()` on **Program.cs** is causing execution errors.

```csharp
...
builder.Services.AddGraphQLServer().AddQueryType<Query>()
                                   //.RegisterDbContextFactory<PostgreContext>() // Execution errors
                                   .AddProjections()
                                   .AddFiltering()
                                   .AddSorting();
...
```

</details>
