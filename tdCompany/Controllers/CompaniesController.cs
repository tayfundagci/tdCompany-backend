using Microsoft.AspNetCore.Mvc;
using MovieApp.Dto;
using MovieApp.Entities;
using MovieApp.Message.Request;
using tdCompany.Interfaces;

namespace MovieApp.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        public CompaniesController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            try
            {
                var companies = await _companyRepo.GetCompanies();

                var response = new Message.Response.CompanyListResponse()
                {
                    Code = 200,
                    Message = "Success",
                    CompanyList = ConvertTo(companies.ToList())
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepo.GetCompany(id);

                if (company == null)
                    return NotFound();

                var response = new Message.Response.CompanyDetailResponse()
                {
                    Code = 200,
                    Message = "Success",
                    Company = new CompanyDto(company)
                };
                
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task <IActionResult> GetCompanyPerson()
        {
            try
            {
                var companyPersonCount = await _companyRepo.GetCompanyPerson();
                return Ok(companyPersonCount);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [Authorize(UserRole.Admin)]
        [HttpPost]
        public async Task <IActionResult> CreateCompany(CompanyCreateRequest request)
        {
            try
            {
                var createdCompany = await _companyRepo.CreateCompany(request);
                var response = new Message.Response.CompanyCreateResponse()
                {
                    Code = 200,
                    Message = "A new company was created",
                    Company = new CompanyDto(createdCompany)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(UserRole.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CompanyUpdateRequest request)
        {   

            try
            {
                var dbCompany = await _companyRepo.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                else
                {
                    request.Id = id;
                    var result = await _companyRepo.UpdateCompany(request);
                    var response = new Message.Response.CompanyUpdateResponse()
                    {
                        Code = 200,
                        Message = "Company updated",
                        Company = new CompanyDto(result)
                    };

                    return Ok(response);
                }
                    

                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(UserRole.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepo.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();

                var response = new Message.Response.CompanyDeleteResponse()
                {
                    Code = 200,
                    Message = "Company deleted",
                };

                await _companyRepo.DeleteCompany(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        public List<CompanyDto> ConvertTo(List<Company> companyList)
        {
            List<CompanyDto> dtoList = new List<CompanyDto>();
            foreach (Company company in companyList)
            {
                dtoList.Add(new CompanyDto(company));
            }
            return dtoList;
        }

    }
}
