﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using XYZUniversity.API.Models.Domain;
using XYZUniversity.API.Models.DTO;
using XYZUniversity.API.Repositories;

namespace XYZUniversity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPaymentRepository paymentRepository;

        public PaymentsController(IMapper mapper, IPaymentRepository paymentRepository)
        {
            this.mapper = mapper;
            this.paymentRepository = paymentRepository;
        }
        //Create Payment
        //POST:/api/payments
        [HttpPost]
        [Authorize(Roles = "Writer")]

        public async Task<IActionResult> Create([FromBody] AddPaymentRequestDto addPaymentRequestDto)
        {
            if (ModelState.IsValid)
            {
                // Map DTO to Domain Model
                var paymentDomainModel = mapper.Map<Payment>(addPaymentRequestDto);

                await paymentRepository.CreateAsync(paymentDomainModel);

                // Map Domain model to DTO
                return Ok(mapper.Map<PaymentDto>(paymentDomainModel));
            }
            return BadRequest();
        }

        // GET All Payments
        //GET:/api/payments
        [HttpGet]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetAllAsync()
        {
            var paymentsDomainModel = await paymentRepository.GetAllAsync();

            //Map domain model to DTO
            return Ok(mapper.Map<List<PaymentDto>>(paymentsDomainModel));

        }

        //Get Payment By Payment ID
        //GET: /api/payments/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var paymentDomainModel = await paymentRepository.GetByIdAsync(id);
            if (paymentDomainModel == null)
            {
                return NotFound();
            }
            //mapp domain model to dto
            return Ok(mapper.Map<PaymentDto>(paymentDomainModel));

        }

        //Update Payments By Id
        //PUT:api/payments/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, UpdatePaymentRequestDto updatePaymentRequestDto)
        {
            if (ModelState.IsValid)
            {
                //Map DTO to Domain Model
                var paymentDomainModel = mapper.Map<Payment>(updatePaymentRequestDto);
                paymentDomainModel = await paymentRepository.UpdateAsync(id, paymentDomainModel);

                if (paymentDomainModel == null)
                {
                    return NotFound();
                }
                //Map Domain Model to DTO

                return Ok(mapper.Map<PaymentDto>(paymentDomainModel));
            }
            return BadRequest();
        }

        // Delete a Payment by Payment ID
        // DELETE: /api/Payments/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deletedWalkDomainModel =   await paymentRepository.DeleteAsync(id);
            if(deletedWalkDomainModel == null)
            {
                return NotFound();
            }
            //Map Domain to Dto
            return Ok(mapper.Map<PaymentDto>(deletedWalkDomainModel));
        }


    }
}